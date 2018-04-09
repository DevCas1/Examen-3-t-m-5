namespace Sjouke.Simple.DOTween
{
    using UnityEngine;
    using DG.Tweening;

    [System.Serializable]
    public sealed class Settings
    {
        public float MoveDuration;
        [Space(10)]
        public Vector3 JumpMoveVector;
        public float JumpMoveDuration;
        [Space(10)]
        public Vector3 TransformMoveOffset;
        [Space(10)]
        public Vector3 SizeIncrement;
        public float SizeJumpDuration;
    }
    
    public class DOTweenAnimator : MonoBehaviour
    {
        public Settings Settings;
        private Vector3 _originalPos;
        private Vector3 _originalScale;

        private void OnEnable()
        {
            _originalPos = transform.localPosition;
            _originalScale = transform.localScale;
        }

        public void MoveX(float addition) => transform.DOBlendableLocalMoveBy(new Vector3(addition, 0, 0), Settings.MoveDuration);

        public void MoveY(float addition) => transform.DOBlendableLocalMoveBy(new Vector3(0, addition, 0), Settings.MoveDuration);

        public void MoveZ(float addition) => transform.DOBlendableMoveBy(new Vector3(0, 0, addition), Settings.MoveDuration);

        public void JumpMove() => transform.DOBlendableLocalMoveBy(Settings.JumpMoveVector.normalized, 
                                                              Settings.JumpMoveDuration).OnComplete(transform.DOBlendableMoveBy(-Settings.JumpMoveVector.normalized, 
                                                                                                                                Settings.JumpMoveDuration).Complete);

        public void MoveToTransorm(Transform target) => transform.DOBlendableLocalMoveBy(transform.InverseTransformPoint(target.position) + Settings.TransformMoveOffset, Settings.MoveDuration);

        public void ReturnToOrigin() => transform.DOLocalMove(_originalPos, Settings.MoveDuration);

        public void JumpSize() => transform.DOScale(_originalScale + Settings.SizeIncrement, Settings.SizeJumpDuration).OnComplete(ResetSize);

        public void ResetSize() => transform.DOScale(_originalScale, Settings.SizeJumpDuration);
    }
}