using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// class to handle unitcollection logic for applying effects on to groups and tracking group count
    /// </summary>
    [CreateAssetMenu(fileName = "new HeroCollection", menuName = "Hero/HeroCollection")]
    public class HeroCollection : InitScriptObject
    {
        /// <summary>
        /// dictionary of unique heroes and the amount of them on the battlefield
        /// </summary>
        Dictionary<string, List<HeroData>> heroesInCollection;
        private List<DragDrop> heroDragDrops;
        public GroupEffect[] groupEffects;
        private GroupEffect currActiveEffect;
        private Dictionary<int, GroupEffect> dicGroupEffect;
        [SerializeField] private Color highlightColor;

        public event Action<HeroCollection> OnFirstUnitPlaced;
        public event Action<HeroCollection> OnCollectionChanged;
        public event Action<HeroCollection> OnLastUnitRemoved;

        public event Action<HeroData> OnAddedToCollection;
        public event Action<HeroData> OnRemovedFromCollection;

        [SerializeField] private int diversity;

        private int lowestDiversity;

        public GroupEffect CurrActiveEffect
        {
            get
            {
                return currActiveEffect;
            }
            set
            {
                if (currActiveEffect != null)
                {
                    currActiveEffect.effect.RemoveEffectFromGroup(this);
                    OnAddedToCollection -= currActiveEffect.effect.ApplyEffect;
                    OnRemovedFromCollection -= currActiveEffect.effect.RemoveEffect;
                }
                currActiveEffect = value;
                if (currActiveEffect != null)
                {
                    currActiveEffect.effect.ApplyEffectToGroup(this);
                    OnAddedToCollection += currActiveEffect.effect.ApplyEffect;
                    OnRemovedFromCollection += currActiveEffect.effect.RemoveEffect;
                }
            }
        }
        public int Diversity
        {
            get => diversity;
            set
            {
                if (dicGroupEffect == null)
                {
                    diversity = value;
                    OnCollectionChanged?.Invoke(this);
                    return;
                }
                //adding herotype
                if (diversity < value)
                {
                    if (!dicGroupEffect.ContainsKey(value))
                    {
                        diversity = value;
                        OnCollectionChanged?.Invoke(this);
                        return;
                    }
                    //if (currActiveEffect != null)
                    //{
                    //    currActiveEffect.effect.RemoveEffectFromGroup(this);
                    //}
                    diversity = value;
                    CurrActiveEffect = dicGroupEffect[diversity];
                    OnCollectionChanged?.Invoke(this);
                }
                //removing herotype
                else if (diversity > value)
                {
                    if (currActiveEffect == null)
                    {
                        diversity = value;
                        OnCollectionChanged?.Invoke(this);
                        return;
                    }
                    if (dicGroupEffect.ContainsKey(diversity) && !dicGroupEffect.ContainsKey(value))
                    {
                        if (value < lowestDiversity)
                        {
                            CurrActiveEffect = null;
                            diversity = value;
                            OnCollectionChanged?.Invoke(this);
                            return;
                        }
                        else
                        {
                            for (int i = value; i >= 0; i--)
                            {
                                if (dicGroupEffect.ContainsKey(i))
                                {
                                    CurrActiveEffect = dicGroupEffect[i];
                                    diversity = value;
                                    OnCollectionChanged?.Invoke(this);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        diversity = value;
                        if (dicGroupEffect.ContainsKey(value))
                        {
                            CurrActiveEffect = dicGroupEffect[diversity];
                        }
                        OnCollectionChanged?.Invoke(this);
                        return;
                    }
                }
            }
        }
        public Dictionary<string, List<HeroData>> HeroesInCollection
        {
            get { return heroesInCollection; }
            set { heroesInCollection = value; }
        }




        /// <summary>
        /// adds a herodata to the collection
        /// </summary>
        /// <param name="_hero">herodata to add</param>
        public void AddToCollection(HeroData _hero, DragDrop _heroDrag)
        {
            if (Diversity == 0)
            {
                OnFirstUnitPlaced?.Invoke(this);
            }
            heroDragDrops.Add(_heroDrag);
            OnAddedToCollection?.Invoke(_hero);
            if (currActiveEffect != null)
            {
               // currActiveEffect.effect.ApplyEffect(_hero);
            }
            if (heroesInCollection.ContainsKey(_hero.name))
            {
                heroesInCollection[_hero.name].Add(_hero);
            }
            else
            {
                heroesInCollection.Add(_hero.name, new List<HeroData>());
                heroesInCollection[_hero.name].Add(_hero);
                Diversity++;
            }

        }
        /// <summary>
        /// removes a herodata from the collection
        /// </summary>
        /// <param name="_hero">herodata to remove</param>
        public void RemoveFromCollection(HeroData _hero, DragDrop _heroDrag)
        {
            heroDragDrops.Remove(_heroDrag);
            heroesInCollection[_hero.name].Remove(_hero);
            if (currActiveEffect != null)
            {
                //currActiveEffect.effect.RemoveEffect(_hero);
            }
            if (heroesInCollection[_hero.name].Count <= 0)
            {
                heroesInCollection.Remove(_hero.name);
                Diversity--;
            }
            OnRemovedFromCollection?.Invoke(_hero);
            if (Diversity == 0)
            {
                OnLastUnitRemoved?.Invoke(this);
            }
        }

        public override void Initialize()
        {
            heroesInCollection = new Dictionary<string, List<HeroData>>();
            heroDragDrops = new List<DragDrop>();
            currActiveEffect = null;
            diversity = 0;
            if (groupEffects.Length == 0)
            {
                return;
            }
            lowestDiversity = int.MaxValue;
            dicGroupEffect = new Dictionary<int, GroupEffect>();
            string namew = this.name;
            for (int i = 0; i < groupEffects.Length; i++)
            {
                dicGroupEffect.Add(groupEffects[i].neededDiversity, groupEffects[i]);
                if (groupEffects[i].neededDiversity < lowestDiversity)
                {
                    lowestDiversity = groupEffects[i].neededDiversity;
                }
            }
        }
        public string GetToolTip()
        {
            string toolTip = this.name + "\n";
            for (int i = 0; i < groupEffects.Length; i++)
            {

                if (currActiveEffect != null && currActiveEffect.neededDiversity == groupEffects[i].neededDiversity)
                {
                    toolTip += "<b>" + "{" + "<u>" + groupEffects[i].neededDiversity.ToString() + "</u>" + "} " + "</b>";
                }
                else
                {
                    toolTip += "{" + groupEffects[i].neededDiversity.ToString() + "} ";
                }
            }
            toolTip += "\n";
            toolTip += "\n";
            if (currActiveEffect != null)
            {
                toolTip += currActiveEffect.effectToolTip;
            }
            else
            {
                toolTip += "\n";
            }
            return toolTip;
        }
        public void TurnOnHighlights()
        {
            for (int i = 0; i < heroDragDrops.Count; i++)
            {
                heroDragDrops[i].ToggleHighlight(true, highlightColor);
            }
        }
        public void TurnOffHighlights()
        {
            for (int i = 0; i < heroDragDrops.Count; i++)
            {
                heroDragDrops[i].ToggleHighlight(false, highlightColor);
            }
        }
    }

    [System.Serializable]
    public class GroupEffect
    {
        public int neededDiversity;
        public Effect effect;
        [TextArea] public string effectToolTip;
    }
}
