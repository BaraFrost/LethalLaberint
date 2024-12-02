using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = nameof(AbilityDataContainer), menuName = "Data/AbilityDataContainer")]
    public class AbilityDataContainer : ScriptableObject {

        [Serializable]
        public struct AbilityDataWithId {
            public int id;
            public AbilityData data;
        }

        [ReadOnly]
        [SerializeField]
        private List<AbilityDataWithId> _abilityDataWithIds;

        private Dictionary<int, AbilityData> _cashedAbilityData;
        private Dictionary<int, AbilityData> CashedAbilityData {
            get {
                if (_cashedAbilityData == null) {
                    _cashedAbilityData = _abilityDataWithIds.ToDictionary(dataWithId => dataWithId.id, dataWithId => dataWithId.data);
                }
                return _cashedAbilityData;
            }
        }

        public int[] GetAllAbilityIds() {
            return CashedAbilityData.Keys.ToArray();
        }

        public AbilityData GetAbility(int id) {
            return CashedAbilityData[id];
        }

#if UNITY_EDITOR
        private void AddAbilityToContainer(AbilityData abilityData) {
            var id = GetNewId();
            abilityData.id = id;
            _abilityDataWithIds.Add(new AbilityDataWithId() {
                id = GetNewId(),
                data = abilityData,
            });
            EditorUtility.SetDirty(abilityData);
            EditorUtility.SetDirty(this);
        }

        private int GetNewId() {
            var maxId = 0;
            foreach (var data in _abilityDataWithIds) {
                if (data.id > maxId) {
                    maxId = data.id;
                }
            }
            return maxId + 1;
        }


        [Button]
        private void UpdateData() {
            RemoveNullElements();
            var guids = AssetDatabase.FindAssets($"t:{nameof(AbilityData)}");
            foreach (var assetGuid in guids) {
                var ability = AssetDatabase.LoadAssetAtPath<AbilityData>(AssetDatabase.GUIDToAssetPath(assetGuid));
                if (ability == null) {
                    continue;
                }
                var abilityInContainer = false;
                foreach (var abilityWithId in _abilityDataWithIds) {
                    if (abilityWithId.data == ability) {
                        abilityInContainer = true;
                        break;
                    }
                }

                if (!abilityInContainer) {
                    AddAbilityToContainer(ability);
                }
            }
        }

        private void RemoveNullElements() {
            for (int i = _abilityDataWithIds.Count - 1; i >= 0; i--) {
                if (_abilityDataWithIds[i].data == null) {
                    _abilityDataWithIds.RemoveAt(i);
                }
            }
        }
#endif
    }
}