using UnityEngine;
using System;

namespace NDTweener
{
    

    public class NDTweenWorker : MonoBehaviour {

        // events + delegates
        public delegate void NDTweenStart();
        public delegate void NDTweenProgress( float progress );
        public delegate void NDTweenComplete();
        
        public event NDTweenStart OnTweenStart;
        public event NDTweenProgress OnTweenProgress;
        public event NDTweenComplete OnTweenComplete;

        // TODO - Make these private OR getters/setters
        public GameObject targetGameObject;
        public Vector3 startPosition, endPosition, startScale, endScale, startRotation, endRotation;
        public Color startColor, endColor; // Only used for 3D tweens
        public float startAlpha, endAlpha; // Only used in UI tweens
        public string colorTarget;
        public bool destroyOnComplete = true;
        public float startTime = 0f;
        public float tweenTimeInSeconds;
        public Func<float, float> easingEquation;
        public float delay = 0f;
        public bool isTo = true;
        public bool isUI = false;
        private bool alphaChildren = true; //Test - apply same alpha value to any CanvasRenderer components in children

        private bool hasRenderer = true;

        public bool clearCurrentTweens = true; // inherited on creation, will remove any previous NDTweenWorker components before starting

        //test
        public bool isBackwards = false;

        private bool isActive = false; // leaving in for the moment, use to add tweens without immediate trigger

        //tween states
        private bool _tweenStarted = false;
        private bool _tweenComplete = false;

        public bool tweenStarted {
            get {
                return _tweenStarted;
            }
            set {
                _tweenStarted = value;
            }
        }

        public bool tweenComplete {
            get {
                return _tweenComplete;
            }
            set {
                _tweenComplete = value;
            }
        }

        /*
        =====
        Public API
        =====
        */

        /*
            starts the tween - generally called via NDTween
            will overwrite start or end values
        */
        public void StartTween( float extraDelay = 0f ) {

            Renderer renderer = CheckForRenderer();

            if(isTo)
            {
                startPosition = GetTargetPosition();
                startScale = GetTargetScale();
                startRotation = GetTargetRotation();
                if(isUI) startAlpha = targetGameObject.GetComponent<CanvasRenderer>().GetAlpha();
                else if(hasRenderer)
                    if (renderer.sharedMaterial.HasProperty("_Color"))
                        startColor = renderer.sharedMaterial.GetColor(colorTarget);
            }
            else
            {
                endPosition = GetTargetPosition();
                endScale = GetTargetScale();
                endRotation = GetTargetRotation();
                if(isUI) endAlpha = targetGameObject.GetComponent<CanvasRenderer>().GetAlpha();
                else if(hasRenderer)
                    if (renderer.sharedMaterial.HasProperty("_Color"))
                        endColor = renderer.sharedMaterial.GetColor(colorTarget);
            }

            
            startTime = Time.time + delay + extraDelay;
            _tweenStarted = _tweenComplete = false;
            
            //let's go!
            isActive = true;
        }

        /*
            Fire the tween again, will reset object to it's start position before doing the same motion 
        */
        public NDTweenWorker Retrigger( float extraDelay = 0f ) {

            // check for any other NDTweenWorker components
            NDTweenWorker[] workers = targetGameObject.GetComponents<NDTweenWorker>();
            bool needsToBeAddedAgain = true;
            // loop through to see if this one exists on targetGameObject
            for( int i = 0; i < workers.Length; i++ ) {
                if(workers[i] == this) needsToBeAddedAgain = false;
            }

            // reset targetGameObject properties
            SetTargetPosition( isTo ? startPosition : endPosition );
            SetTargetScale( isTo ? startScale : endScale );
            SetTargetRotation( isTo ? startRotation : endRotation );
            if( isUI ) targetGameObject.GetComponent<CanvasRenderer>().SetAlpha( isTo ? startAlpha : endAlpha );
            else if( hasRenderer ) targetGameObject.GetComponent<Renderer>().material.SetColor( colorTarget, isTo ? startColor : endColor );
            
            //if not present, fire through NDTween as initially fired
            if( needsToBeAddedAgain ) {
                if(isUI)
                {
                    if(isTo) return NDUITween.To( targetGameObject, tweenTimeInSeconds, endPosition, endScale, endRotation, endAlpha, easingEquation, delay, destroyOnComplete, clearCurrentTweens );
                    else return NDUITween.From( targetGameObject, tweenTimeInSeconds, startPosition, startScale, startRotation, startAlpha, easingEquation, delay, destroyOnComplete, clearCurrentTweens );
                }
                else
                {
                    if(isTo) return NDTween.To( targetGameObject, tweenTimeInSeconds, endPosition, endScale, endRotation, endColor, colorTarget, easingEquation, delay, destroyOnComplete, clearCurrentTweens );
                    else return NDTween.From( targetGameObject, tweenTimeInSeconds, startPosition, startScale, startRotation, startColor, colorTarget, easingEquation, delay, destroyOnComplete, clearCurrentTweens );
                }
            }
            else
            {
                //otherwise call StartTween
                StartTween();
                return this;
            }

        }

        public void JumpToEndState() {
            isActive = false;
            // SetTargetPosition( endPosition );
            // SetTargetScale( endScale );
            // SetTargetRotation( endRotation );
            SetObjectTweenState( 1f );
        }

        /*
            New methods in place of FromTo
            These don't grab states on being called but use the start and end values in order to move around the tween 
        */

        public void PlayTween( float extraDelay = 0f )
        {
            CheckForRenderer();

            isBackwards = false;
            SetObjectTweenState( 0f );

            startTime = Time.time + delay + extraDelay;
            _tweenStarted = _tweenComplete = false;
            
            //let's go!
            isActive = true;
        }

        public void PlayTweenReverse( float extraDelay = 0f )
        {
            CheckForRenderer();

            isBackwards = true;
            SetObjectTweenState( 1f );

            startTime = Time.time + delay + extraDelay;
            _tweenStarted = _tweenComplete = false;
            
            //let's go!
            isActive = true;
        }

        public void SetProgress( float progress ) 
        {
            if( progress < 0f ) Debug.LogWarning("Value will be clamped to 0");
            else if( progress > 1f ) Debug.LogWarning("Value will be clamped to 1");

            float safeProgress = Mathf.Clamp(progress, 0f, 1f);
            SetObjectTweenState( safeProgress );
        }

        public void ResetTween() 
        {
            SetObjectTweenState( 0f );
        }

        /*
        =====
        Private Methods
        =====
        */

        /*
            Monobehaviour Update call
        */
        void Update() {

            // end here if StartTween has not been called yet
            if( !isActive ) return;
            
            // calculate elapsed time since tween start
            float timeElapsed = Time.time - startTime;
            float progress = timeElapsed / tweenTimeInSeconds;
            
            if(progress < 0f ) {
                //tween hasn't started yet (delay in effect)
                progress = 0f;
            }
            else if( (progress > 0f && progress <1f ) && !_tweenStarted ) {
                //tween has started, update state + broadcast start event
                _tweenStarted = true;
				SetObjectTweenStartStates();
                if( OnTweenStart != null ) OnTweenStart();
            }
            else if(progress > 1f) {
                //cap progress at 1f
                progress = 1f;
            }

            //storing progress for check if reversing
            float preProgress = progress;

            // apply easing to the progress figure
            progress = easingEquation(progress);

            // reverse the progress value if isBackwards
            if( isBackwards ) progress = 1f - progress;

            //broadcast progress event
            if( OnTweenProgress != null ) OnTweenProgress( progress );

            // update object state
            SetObjectTweenState( progress );

            //tween is complete
            if(preProgress == 1f) 
            {
                //broadcase complete event
                if( OnTweenComplete != null ) OnTweenComplete();
                //update tween states
                _tweenComplete = true;
                isActive = false;
                //destroy component if required
                if( destroyOnComplete ) Destroy( this );
            }
        }

		private void SetObjectTweenStartStates ()
		{
			Renderer renderer = CheckForRenderer();

			if ( isTo )
			{
				startPosition = GetTargetPosition();
				startScale = GetTargetScale();
				startRotation = GetTargetRotation();
				if ( isUI ) startAlpha = targetGameObject.GetComponent<CanvasRenderer>().GetAlpha();
				else if ( hasRenderer )
					if ( renderer.sharedMaterial.HasProperty( "_Color" ) )
						startColor = renderer.sharedMaterial.GetColor( colorTarget );
			}
			else
			{
				endPosition = GetTargetPosition();
				endScale = GetTargetScale();
				endRotation = GetTargetRotation();
				if ( isUI ) endAlpha = targetGameObject.GetComponent<CanvasRenderer>().GetAlpha();
				else if ( hasRenderer )
					if ( renderer.sharedMaterial.HasProperty( "_Color" ) )
						endColor = renderer.sharedMaterial.GetColor( colorTarget );
			}
		}

        private void SetObjectTweenState( float progress )
        {

            //update targetGameObject properties with lerped values if needed
            if( startPosition != endPosition )    SetTargetPosition( Vector3.Lerp(startPosition, endPosition, progress) );
            if( startScale != endScale )          SetTargetScale( Vector3.Lerp(startScale, endScale, progress) );
            if( startRotation != endRotation ){
                Vector3 rot = new Vector3(
                    Mathf.LerpAngle(startRotation.x, endRotation.x, progress),
                    Mathf.LerpAngle(startRotation.y, endRotation.y, progress),
                    Mathf.LerpAngle(startRotation.z, endRotation.z, progress)
                );
                //Mathf.LerpAngle(startRotation, endRotation, progress)
                SetTargetRotation( rot );
            }
            
            if(isUI) // Only handling CanvasRenderer alpha values for a UI tween
            {
                if(startAlpha != endAlpha)
                {
                    float newAlpha = Mathf.Lerp(startAlpha, endAlpha, progress);
                    targetGameObject.GetComponent<CanvasRenderer>().SetAlpha( newAlpha );
                    if(alphaChildren) UpdateChildrenAlphas( newAlpha );
                }
            }
            else // Material tween properties for 3D elements
            {
                if( hasRenderer && (startColor != endColor) ) {
                    Color newColor = Color.Lerp(startColor, endColor, progress);
                    Renderer rend = targetGameObject.GetComponent<Renderer>();
                    rend.material.SetColor( colorTarget, newColor );
                }
            }

        }


        private void UpdateChildrenAlphas( float alpha ){
            CanvasRenderer[] canvasChildren = GetComponentsInChildren<CanvasRenderer>();
            for(int i = 0; i < canvasChildren.Length; i++)
            {
                canvasChildren[i].SetAlpha( alpha );
            }
        }


        private Renderer CheckForRenderer() {
            Renderer renderer = targetGameObject.GetComponent<Renderer>();
            if(renderer == null) {
                hasRenderer = false;
                return null;
            }

            return renderer;
        }


        /*
            Using these as getters / setters to apply transform updates to the correct component
        */
        private Vector3 GetTargetPosition(){
            if(isUI) return targetGameObject.GetComponent<RectTransform>().anchoredPosition;
            else return targetGameObject.transform.localPosition;
        }

        private void SetTargetPosition( Vector3 position ){
            if(isUI) targetGameObject.GetComponent<RectTransform>().anchoredPosition = position;
            else targetGameObject.transform.localPosition = position;
        }

        private Vector3 GetTargetScale(){
            if(isUI) return targetGameObject.GetComponent<RectTransform>().localScale;
            else return targetGameObject.transform.localScale;
        }

        private void SetTargetScale( Vector3 scale ){
			if ( float.IsNaN(scale.x) && !float.IsNaN(scale.y) && !float.IsNaN(scale.z) )
			{
				if ( isUI ) targetGameObject.GetComponent<RectTransform>().localScale = scale;
				else targetGameObject.transform.localScale = scale;
			}
        }

        private Vector3 GetTargetRotation(){
            if(isUI) return targetGameObject.GetComponent<RectTransform>().localRotation.eulerAngles;
            else return targetGameObject.transform.localRotation.eulerAngles;
        }

        private void SetTargetRotation( Vector3 eulerRotation ){
            if(isUI) targetGameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler( eulerRotation );
            else targetGameObject.transform.localRotation = Quaternion.Euler( eulerRotation );
        }

    }   
}