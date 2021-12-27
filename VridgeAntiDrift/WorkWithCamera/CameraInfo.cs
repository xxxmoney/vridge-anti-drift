using Android.Hardware;
using Android.Hardware.Camera2;

namespace Task2
{
    public class CameraInfo
    {
        CameraManager _cameraManager;
        Camera.CameraInfo _info;

        public CameraInfo(CameraManager cameraManager)
        {
            _cameraManager = cameraManager;
            _info = new Camera.CameraInfo();
        }

        public CameraFacing GetCameraFacing(int cameraID)
        {
            Camera.GetCameraInfo(cameraID, _info);
            return _info.Facing;
        }
        
        public int NumberOfCameras()
            => _cameraManager.GetCameraIdList().Length;
        
        public int[] GetCameraIdArray()
        {
            string[] idList = _cameraManager.GetCameraIdList();
            var array = new int[idList.Length];

            for (int i = 0; i < idList.Length; i++)
                array[i] = int.Parse(idList[i]);

            return array;
        }
    }
}