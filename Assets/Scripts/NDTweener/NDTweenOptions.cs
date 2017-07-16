
using System;

namespace NDTweener
{
    
    public class NDTweenOptions {

        private Func<float, float> _easing = null;
        private float _delay = 0f;
        private bool _destroyOnComplete = true;
        private bool _clearCurrentTweens = true;
        private bool _autoPlay = true;
        private bool _isUI = false;
        private bool _isGlobal = false;

        public Func<float, float> easing {
            get {
                return _easing;
            }
            set {
                _easing = value;
            }
        }

        public float delay {
            get {
                return _delay;
            }
            set {
                _delay = value;
            }
        }

        public bool destroyOnComplete {
            get {
                return _destroyOnComplete;
            }
            set {
                _destroyOnComplete = value;
            }
        }

        public bool clearCurrentTweens {
            get {
                return _clearCurrentTweens;
            }
            set {
                _clearCurrentTweens = value;
            }
        }

        public bool autoPlay {
            get {
                return _autoPlay;
            }
            set {
                _autoPlay = value;
            }
        }

        public bool isUI {
            get {
                return _isUI;
            }

            set {
                _isUI = value;
            }
        }

        public bool isGlobal
        {
            get
            {
                return _isGlobal;
            }

            set
            {
                _isGlobal = value;
            }
        }

        public NDTweenOptions( Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ){

            _easing = easing;
            _delay = delay;
            _destroyOnComplete = destroyOnComplete;
            _clearCurrentTweens = clearCurrentTweens;
            _autoPlay = autoPlay;

        } 


    }

}