using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class CellEnvironment : MonoBehaviour {

        [SerializeField]
        private List<LabyrinthCell.Direction> _directions;
        public List<LabyrinthCell.Direction> Directions => _directions;
    }
}
