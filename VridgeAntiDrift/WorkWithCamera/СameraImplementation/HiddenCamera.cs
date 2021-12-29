using Android.Hardware;
using Android.Hardware.Camera2;
using Android.Util;
using Java.IO;
using Java.Lang;
using System.Linq;

namespace Task2
{
    public partial class HiddenCamera
    {
        const int SURFACE_TEXTURE_NAME = 10;
        const int JPEG_MAX_QUALITY = 70;

        Camera _camera;
        CameraInfo _cameraInfo;
        CameraFacing _currentCameraFacing;
        Camera.IShutterCallback _shutterCallback;
        Camera.IPictureCallback _rawPictureCallback;

        private bool CameraIsOpen
        {
            get => _camera != null;
        }

        public HiddenCamera(CameraManager cameraManager)
        {
            _cameraInfo = new CameraInfo(cameraManager);
            _shutterCallback = null;
            _rawPictureCallback = null;
        }
        
        private int GetBackCameraId()
        {
            return _cameraInfo.GetCameraIdArray().First(x => _cameraInfo.GetCameraFacing(x) == CameraFacing.Back); 
        }

        public void TakePhoto()
        {
            int cameraId = this.GetBackCameraId();
            SafeCameraOpen(cameraId);

            if (CameraIsOpen)
            {
                _currentCameraFacing = _cameraInfo.GetCameraFacing(cameraId);
                SetCameraParametersAndStartPreview();
                TakePicture(cameraId);
            }
        }
        
        public void StopPreviewAndFreeCamera()
        {
            if (CameraIsOpen)
            {
                _camera.StopPreview();
                _camera.Release();
                _camera = null;
            }
        }

        private void ReleaseCamera()
        {
            if (CameraIsOpen)
            {
                _camera.Release();
                _camera = null;
            }
        }
       
        private void SafeCameraOpen(int id)
        {
            try
            {
                ReleaseCamera();
                _camera = Camera.Open(id);
            }
            catch (Exception e)
            {
                Log.Info(Constants.CAMERA_TAG, "Problem while opening the camera.");
                e.PrintStackTrace();
            }
        }

        private void SetCameraParametersAndStartPreview()
        {
            try
            {
                SetCameraParameters();
                _camera.SetPreviewTexture(new Android
                                              .Graphics
                                              .SurfaceTexture(SURFACE_TEXTURE_NAME));
            }
            catch (IOException e)
            {
                e.PrintStackTrace();
            }

            _camera.StartPreview();
        }

        private void SetCameraParameters()
        {
            Camera.Parameters parameters = _camera.GetParameters();
            ModifyParameters(parameters);
            _camera.SetParameters(parameters);
        }

        private void TakePicture(int cameraId)
            => _camera.TakePicture(
                    _shutterCallback, 
                    _rawPictureCallback, 
                    new PictureCallback(cameraId));

        partial void ModifyParameters(Camera.Parameters oldParameters);
    }
}