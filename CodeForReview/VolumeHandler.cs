using Project.Common.ExtensionMethods;
using Project.Common.Structs;
using UnityEngine.Audio;

namespace Project.Core.AudioSystem
{
    public class VolumeHandler
    {
        private AudioMixer audioMixer;
        private RangeFloat volumeRange;

        public VolumeHandler(AudioMixer audioMixer, RangeFloat volumeRange)
        {
            this.audioMixer = audioMixer;
            this.volumeRange = volumeRange;
        }

        public void SetVolume(VolumeType volumeType, float newVolume)
        {
            var newVolumeWithinRange = newVolume.Map(volumeRange.min, volumeRange.max);
            audioMixer.SetFloat(GetVolumeString(volumeType), newVolumeWithinRange);
        }

        public float GetVolume(VolumeType volumeType)
        {
            audioMixer.GetFloat(GetVolumeString(volumeType), out var volume);
            volume = volume.Map(0, 1, volumeRange.min, volumeRange.max);
            return volume;
        }

        private string GetVolumeString(VolumeType volumeType) => $"{volumeType}Volume";
    }
}