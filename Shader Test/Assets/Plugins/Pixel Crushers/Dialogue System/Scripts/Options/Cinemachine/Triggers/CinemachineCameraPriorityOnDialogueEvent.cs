
using UnityEngine;

namespace PixelCrushers.DialogueSystem
{

    [AddComponentMenu("")] // Use wrapper. Cinemachine.CinemachineFreeLook freeLookCam;
    public class CinemachineCameraPriorityOnDialogueEvent : ActOnDialogueEvent
    {

        [Tooltip("The Cinemachine virtual camera whose priority to control.")]
        public Cinemachine.CinemachineVirtualCamera virtualCamera;

        [Tooltip("Set the virtual camera to this priority when the start event occurs.")]
        public int onStart = 99;

        [Tooltip("Set the virtual camera to this priority when the end event occurs.")]
        public int onEnd = 0;

        public override void TryStartActions(Transform actor)
        {
            if (virtualCamera == null) return;
            virtualCamera.Priority = onStart;
        }

        public override void TryEndActions(Transform actor)
        {
            if (virtualCamera == null) return;
            virtualCamera.Priority = onEnd;
        }
    }
}