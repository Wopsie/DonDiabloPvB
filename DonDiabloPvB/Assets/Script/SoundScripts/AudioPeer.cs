using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPeer : MonoBehaviour {
    public AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float[] _freqBand = new float[8];
    public static float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float[8];

    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];

    public static float _Amplitude, _AmplitudeBuffer;
    private float _AmplitudeHighest;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
	}

    void GetAmplitude() {
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }
        if (_CurrentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _CurrentAmplitude;
        }
        _Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
    }

    void CreateAudioBands() {
        for (int i = 0; i < 8; i++) {
            if (_freqBand[i] > _freqBandHighest[i]) {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }


    void GetSpectrumAudioSource() {
       _audioSource.GetSpectrumData(_samples,0,FFTWindow.BlackmanHarris);
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            if (_freqBand[g] > _bandBuffer[g]) {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }

            else if (_freqBand[g] < _bandBuffer[g]) {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void MakeFrequencyBands()
    { /*
        * 22050 / 512 = 43hertz per sample
        * 
        * 20 - 60 hertz - Sub Bass ------------ The sub bass provides the first usable low frequencies on most recordings. The deep bass produced in this range is usually felt more than it is heard, providing a sense of power.
        * 60 - 250 hertz - Bass --------------- The bass range determines how fat or thin the sound is. The fundamental notes of rhythm are centered on this area. Most bass signals in modern music tracks lie around the 90-200 Hz area.
        * 250 - 500 hertz - Low Midrange------- The low midrange contains the low order harmonics of most instruments and is generally viewed as the bass presence range.
        * 500 - 2000 hertz - Midrange --------- The midrange determines how prominent an instrument is in the mix. Boosting around 1000 Hz can give instruments a horn like quality.
        * 2000 - 4000 hertz - Upper Midrange -- Human hearing is extremely sensitive at the high midrange frequencies, with the slightest boost around here resulting in a huge change in the sound timbre.
        * 4000 - 6000 hertz - Presence -------- The presence range is responsible for clarity and definition of a sound. It is the range at which most home stereos center their treble control on.
        * 6000 - 20000 hertz - Brillance ------ The brilliance range is composed entirely of harmonics and is responsible for sparkle and air of a sound. Boost around 12 kHz make a recording sound more Hi Fi.
        * 
        * 0 - 2 = 86 hertz
        * 1 - 4 = 172 hertz - 87-258
        * 2 - 8 = 344 hertz -  259 - 602
        * 3 - 16 = 688 hertz - 603 - 1290
        * 4 - 32 = 1376 hertz - 1291 - 2666
        * 5 - 64 = 2752 hertz - 2667 - 5418
        * 6 - 128 = 5504 hertz - 5419 - 10922
        * 7 - 256 = 11008 hertz - 10923 - 21930
        * 518
        */

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, 1) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;

            _freqBand[i] = average * 10;
        }
    }
}
