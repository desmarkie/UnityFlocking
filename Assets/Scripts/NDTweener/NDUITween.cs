using System;
using UnityEngine;

namespace NDTweener
{
    

    public class NDUITween {

        //position, scale, rotation, alpha

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector2 position, NDTweenOptions opts ) {
            
            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.To( target, timeInSeconds, position, rTransform.localScale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector2 position, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {

            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.To( target, timeInSeconds, position, rTransform.localScale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, NDTweenOptions opts ) {
            
            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.To( target, timeInSeconds, position, scale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {

            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.To( target, timeInSeconds, position, scale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, Vector3 rotation, NDTweenOptions opts ) {
            
            return NDUITween.To( target, timeInSeconds, position, scale, rotation, NDUITween.GetCurrentAlpha(target), opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, Vector3 rotation, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {

            return NDUITween.To( target, timeInSeconds, position, scale, rotation, NDUITween.GetCurrentAlpha(target), easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, Vector3 rotation, float alpha, NDTweenOptions opts ) {

            return NDUITween.To( target, timeInSeconds, position, scale, rotation, alpha, opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay );
        }

        static public NDTweenWorker ScaleTo( GameObject target, float timeInSeconds, Vector2 scale, NDTweenOptions opts ) {

            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.To( target, timeInSeconds, rTransform.anchoredPosition, scale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay);
        }

        static public NDTweenWorker ScaleTo( GameObject target, float timeInSeconds, Vector2 scale, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {
            
            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.To( target, timeInSeconds, rTransform.anchoredPosition, scale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay);
        }

        static public NDTweenWorker RotateTo( GameObject target, float timeInSeconds, Vector3 rotation, NDTweenOptions opts ) {

            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.To( target, timeInSeconds, rTransform.anchoredPosition, rTransform.localScale, rotation, NDUITween.GetCurrentAlpha(target), opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay);
        }

        static public NDTweenWorker RotateTo( GameObject target, float timeInSeconds, Vector3 rotation, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {
            
            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.To( target, timeInSeconds, rTransform.anchoredPosition, rTransform.localScale, rotation, NDUITween.GetCurrentAlpha(target), easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay);
        }

		static public NDTweenWorker AlphaTo ( GameObject target, float timeInSeconds, float alpha, NDTweenOptions opts )
		{

			RectTransform rTransform = target.GetComponent<RectTransform>();
			return NDUITween.To( target, timeInSeconds, rTransform.anchoredPosition, rTransform.localScale, rTransform.localRotation.eulerAngles, alpha, opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay );
		}

		static public NDTweenWorker AlphaTo ( GameObject target, float timeInSeconds, float alpha, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true )
		{

			RectTransform rTransform = target.GetComponent<RectTransform>();
			return NDUITween.To( target, timeInSeconds, rTransform.anchoredPosition, rTransform.localScale, rTransform.localRotation.eulerAngles, alpha, easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay );
		}

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, Vector3 rotation, float alpha, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {

            return NDUITween.CreateTweenWorker(
                true,
                target,
                position,
                scale,
                rotation,
                alpha,
                timeInSeconds,
                easing,
                delay,
                destroyOnComplete,
                clearCurrentTweens,
                autoPlay
            );
        }


        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector2 position, NDTweenOptions opts ) {
            
            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.From( target, timeInSeconds, position, rTransform.localScale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay );
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector2 position, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {

            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.From( target, timeInSeconds, position, rTransform.localScale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay );
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, NDTweenOptions opts ) {
            
            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.From( target, timeInSeconds, position, scale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay );
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {

            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.From( target, timeInSeconds, position, scale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay );
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, Vector3 rotation, NDTweenOptions opts ) {
            
            return NDUITween.From( target, timeInSeconds, position, scale, rotation, NDUITween.GetCurrentAlpha(target), opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay );
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, Vector3 rotation, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {

            return NDUITween.From( target, timeInSeconds, position, scale, rotation, NDUITween.GetCurrentAlpha(target), easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay );
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, Vector3 rotation, float alpha, NDTweenOptions opts ) {

            return NDUITween.From( target, timeInSeconds, position, scale, rotation, alpha, opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay );
        }

         static public NDTweenWorker ScaleFrom( GameObject target, float timeInSeconds, Vector2 scale, NDTweenOptions opts ) {

            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.From( target, timeInSeconds, rTransform.anchoredPosition, scale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay);
        }

        static public NDTweenWorker ScaleFrom( GameObject target, float timeInSeconds, Vector2 scale, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {
            
            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.From( target, timeInSeconds, rTransform.anchoredPosition, scale, rTransform.localRotation.eulerAngles, NDUITween.GetCurrentAlpha(target), easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay);
        }

        static public NDTweenWorker RotateFrom( GameObject target, float timeInSeconds, Vector3 rotation, NDTweenOptions opts ) {

            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.From( target, timeInSeconds, rTransform.anchoredPosition, rTransform.localScale, rotation, NDUITween.GetCurrentAlpha(target), opts.easing, opts.delay, opts.destroyOnComplete, opts.clearCurrentTweens, opts.autoPlay);
        }

        static public NDTweenWorker RotateFrom( GameObject target, float timeInSeconds, Vector3 rotation, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {
            
            RectTransform rTransform = target.GetComponent<RectTransform>();
            return NDUITween.From( target, timeInSeconds, rTransform.anchoredPosition, rTransform.localScale, rotation, NDUITween.GetCurrentAlpha(target), easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay);
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector2 position, Vector2 scale, Vector3 rotation, float alpha, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {

            return NDUITween.CreateTweenWorker(
                false,
                target,
                position,
                scale,
                rotation,
                alpha,
                timeInSeconds,
                easing,
                delay,
                destroyOnComplete,
                clearCurrentTweens,
                autoPlay
            );
        }



        static private NDTweenWorker CreateTweenWorker( bool isTo, GameObject target, Vector3 position, Vector3 scale, Vector3 rotation, float alpha, float timeInSeconds, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = false, bool autoPlay = true ) {

            //remove any previous tweens if needed
            if(clearCurrentTweens) NDTween.RemoveAllTweens( target );
            
            //create the component and add to the target game object
            NDTweenWorker tweenWorker = target.AddComponent<NDTweenWorker>();
            tweenWorker.targetGameObject = target;

            //set tween properties
            tweenWorker.startPosition = isTo ? target.GetComponent<RectTransform>().localPosition : position;
            tweenWorker.endPosition = isTo ? position : target.GetComponent<RectTransform>().localPosition;

            tweenWorker.startScale = isTo ? target.GetComponent<RectTransform>().localScale : scale;
            tweenWorker.endScale = isTo ? scale : target.GetComponent<RectTransform>().localScale;

            tweenWorker.startRotation = isTo ? target.GetComponent<RectTransform>().localRotation.eulerAngles : rotation;
            tweenWorker.endRotation = isTo ? rotation : target.GetComponent<RectTransform>().localRotation.eulerAngles;

            tweenWorker.startAlpha = isTo ? target.GetComponent<CanvasRenderer>().GetAlpha() : alpha;
            tweenWorker.endAlpha = isTo ? alpha : target.GetComponent<CanvasRenderer>().GetAlpha();

            tweenWorker.tweenTimeInSeconds = timeInSeconds;
            tweenWorker.delay = delay;

            tweenWorker.isTo = isTo;

            tweenWorker.isUI = true;

            tweenWorker.easingEquation = NDTween.GetEasingEquation( easing );

            tweenWorker.destroyOnComplete = destroyOnComplete;

            //start it
            if( autoPlay) tweenWorker.StartTween();

            //return reference
            return tweenWorker;

        }

        static private float GetCurrentAlpha( GameObject target ){
			CanvasRenderer rend = target.GetComponent<CanvasRenderer>();
			if ( rend ) return rend.GetAlpha();
			else return 1f;
        }

    }
    


}