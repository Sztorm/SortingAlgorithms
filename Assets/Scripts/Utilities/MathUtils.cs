using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SortingAlgorithms
{
    public static class MathUtils
    {
        public static float Remap(float value, Vector2 inMinMax, Vector2 outMinMax)
            => outMinMax.x + (value - inMinMax.x) *
               (outMinMax.y - outMinMax.x) / (inMinMax.y - inMinMax.x);

        public static Keyframe Clamp01(Keyframe keyframe)
        {
            keyframe.time = Mathf.Clamp01(keyframe.time);
            keyframe.value = Mathf.Clamp01(keyframe.value);

            return keyframe;
        }

        public static AnimationCurve Clamp01(AnimationCurve curve)
        {
            int length = curve.length;
            var keyframes = curve.keys;

            for (int i = 0; i < length; i++)
            {
                keyframes[i] = Clamp01(curve.keys[i]);
            }
            return new AnimationCurve(keyframes);
        }

        public static Vector4 DivideComponents(Vector4 right, Vector4 left)
            => new Vector4(right.x / left.x, right.y / left.y, right.z / left.z, right.w / left.w);

        public static Vector3 DivideComponents(Vector3 right, Vector3 left)
            => new Vector3(right.x / left.x, right.y / left.y, right.z / left.z);

        public static Vector2 DivideComponents(Vector2 right, Vector2 left)
            => new Vector2(right.x / left.x, right.y / left.y);
    }
}