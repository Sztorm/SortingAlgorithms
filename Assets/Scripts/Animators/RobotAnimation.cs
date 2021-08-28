using System;
using UnityEngine;

namespace SortingAlgorithms
{
    public readonly struct RobotAnimation : IEquatable<RobotAnimation>
    {
        public static readonly RobotAnimation PickUp = new RobotAnimation(
            Animator.StringToHash("PickUp"));

        public static readonly RobotAnimation Walk = new RobotAnimation(
            Animator.StringToHash("Walk"));

        public static readonly RobotAnimation Idle = new RobotAnimation(
            Animator.StringToHash("Idle"));

        public readonly int HashValue;

        private RobotAnimation(int hashValue) => HashValue = hashValue;

        public bool Equals(RobotAnimation other) => HashValue == other.HashValue;

        public override bool Equals(object obj)
        {
            if (obj != null && obj is RobotAnimation animation)
            {
                return Equals(animation);
            }
            return false;
        }

        public override int GetHashCode() => HashValue.GetHashCode();

        public static bool operator ==(RobotAnimation left, RobotAnimation right)
            => left.Equals(right);

        public static bool operator !=(RobotAnimation left, RobotAnimation right)
            => !left.Equals(right);
    }
}