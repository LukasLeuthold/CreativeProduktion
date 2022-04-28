using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// class to handle unitcollection logic for applying effects on to groups and tracking group count
    /// </summary>
    [CreateAssetMenu(fileName = "new HeroCollection", menuName = "ScriptableCollection/HeroCollection", order = 1)]
    public class HeroCollection : InitScriptObject
    {
        //TODO: list for testing purpose only used for visualizing the dictionary
        public List<HeroData> heroessssTest;
        //
        /// <summary>
        /// dictionary of unique heroes and the amount of them on the battlefield
        /// </summary>
        Dictionary<string, List<HeroData>> heroesInCollection;
        private List<DragDrop> heroDragDrops;
        public GroupEffect[] groupEffects;
        private Effect currEffect;
        private string currEffectToolTip;
        private int currNeededDiversity;
        private Dictionary<int, GroupEffect> dicGroupEffect;
        [SerializeField]private Color highlightColor;

        public event Action<HeroCollection> OnFirstUnitPlaced;
        public event Action<HeroCollection> OnCollectionChanged;
        public event Action<HeroCollection> OnLastUnitRemoved;

        [SerializeField] private int diversity;

        private int lowestDiversity;

        public Effect CurrEffect
        {
            get
            {
                return currEffect;
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
                if (diversity < value)
                {
                    if (!dicGroupEffect.ContainsKey(value))
                    {
                        diversity = value;
                        OnCollectionChanged?.Invoke(this);
                        return;
                    }
                    if (currEffect != null)
                    {
                        RemoveEffectFromGroup(currEffect);
                    }
                    diversity = value;
                    currEffect = dicGroupEffect[diversity].effect;
                    currEffectToolTip = dicGroupEffect[diversity].effectToolTip;
                    currNeededDiversity = diversity;
                    OnCollectionChanged?.Invoke(this);
                    ApplyEffectToGroup(currEffect);
                }
                else if (diversity > value)
                {
                    if (currEffect == null)
                    {
                        diversity = value;
                        OnCollectionChanged?.Invoke(this);
                        return;
                    }
                    if (dicGroupEffect.ContainsKey(diversity) && !dicGroupEffect.ContainsKey(value))
                    {
                        if (value < lowestDiversity)
                        {
                            RemoveEffectFromGroup(currEffect);
                            currEffect = null;
                            currEffectToolTip = null;
                            diversity = value;
                            OnCollectionChanged?.Invoke(this);
                            return;
                        }
                        else
                        {
                            RemoveEffectFromGroup(currEffect);
                            for (int i = value; i >= 0; i--)
                            {
                                if (dicGroupEffect.ContainsKey(i))
                                {
                                    currEffect = dicGroupEffect[i].effect;
                                    currEffectToolTip = dicGroupEffect[i].effectToolTip;
                                    currNeededDiversity = i;
                                    diversity = value;
                                    OnCollectionChanged?.Invoke(this);
                                    ApplyEffectToGroup(currEffect);
                                    return;
                                }
                            }
                        }
                    }
                    if (!dicGroupEffect.ContainsKey(diversity) && !dicGroupEffect.ContainsKey(value))
                    {
                        diversity = value;
                        OnCollectionChanged?.Invoke(this);
                        return;
                    }
                    if (!dicGroupEffect.ContainsKey(diversity) && dicGroupEffect.ContainsKey(value))
                    {
                        diversity = value;
                        OnCollectionChanged?.Invoke(this);
                        return;
                    }
                }
            }
        }


        /// <summary>
        /// adds a herodata to the collection
        /// </summary>
        /// <param name="_hero">herodata to add</param>
        public void AddToCollection(HeroData _hero,DragDrop _heroDrag)
        {
            if (Diversity == 0)
            {
                OnFirstUnitPlaced?.Invoke(this);
            }
            heroDragDrops.Add(_heroDrag);
            heroessssTest.Add(_hero);
            if (currEffect != null)
            {
                currEffect.ApplyEffect(_hero);
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
            heroessssTest.Remove(_hero);
            heroDragDrops.Remove(_heroDrag);
            heroesInCollection[_hero.name].Remove(_hero);
            if (currEffect != null)
            {
                currEffect.RemoveEffect(_hero);
            }
            if (heroesInCollection[_hero.name].Count <= 0)
            {
                heroesInCollection.Remove(_hero.name);
                Diversity--;
            }
            if (Diversity == 0)
            {
                OnLastUnitRemoved?.Invoke(this);
            }
        }

        public override void Initialize()
        {
            heroesInCollection = new Dictionary<string, List<HeroData>>();
            heroDragDrops = new List<DragDrop>();
            currEffect = null;
            currEffectToolTip = null;
            diversity = 0;
            heroessssTest = new List<HeroData>();
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

        private void ApplyEffectToGroup(Effect _effect)
        {
            for (int i = 0; i < heroesInCollection.Count; i++)
            {
                List<HeroData> heroes = heroesInCollection.ElementAt(i).Value;
                for (int j = 0; j < heroes.Count; j++)
                {
                    _effect.ApplyEffect(heroes[j]);
                }
            }
        }
        private void RemoveEffectFromGroup(Effect _effect)
        {
            for (int i = 0; i < heroesInCollection.Count; i++)
            {
                List<HeroData> heroes = heroesInCollection.ElementAt(i).Value;
                for (int j = 0; j < heroes.Count; j++)
                {
                    _effect.RemoveEffect(heroes[j]);
                }
            }
        }

        public string GetToolTip()
        {
            string toolTip = this.name;
            for (int i = 0; i < groupEffects.Length; i++)
            {
                if (diversity == groupEffects[i].neededDiversity)
                {
                    toolTip += "<b>"+ " {"  + "<u>"+ groupEffects[i].neededDiversity.ToString() + "</u>" + "} "+ "</b>";
                }
                else
                {
                    toolTip += " {" + groupEffects[i].neededDiversity.ToString() + "} ";
                }
            }
            toolTip += "\n";
            toolTip += "\n";
            if (currEffectToolTip != null)
            {
                toolTip += currEffectToolTip;
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
                heroDragDrops[i].ToggleHighlight(true,highlightColor);
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
        public string effectToolTip;
    }
}
