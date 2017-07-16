using System;
using UnityEngine;

namespace NDTweener
{

    public class NDTween {

        /*
        	Tween a game object TO the supplied values FROM it's current state
        */

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector3 position, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false, bool isGlobal = false ) {

            return NDTween.To(target, timeInSeconds, position, target.transform.localScale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector3 position, NDTweenOptions options ) {

            return NDTween.To(target, timeInSeconds, position, target.transform.localScale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.To(target, timeInSeconds, position, scale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, NDTweenOptions options ) {

            return NDTween.To(target, timeInSeconds, position, scale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI);
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.To( target, timeInSeconds, position, scale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, NDTweenOptions options ) {

            return NDTween.To( target, timeInSeconds, position, scale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI);
        }

        static public NDTweenWorker ScaleTo( GameObject target, float timeInSeconds, Vector3 scale, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {
            
            return NDTween.To( target, timeInSeconds, target.transform.localPosition, scale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker ScaleTo( GameObject target, float timeInSeconds, Vector3 scale, NDTweenOptions options ) {
            
            return NDTween.To( target, timeInSeconds, target.transform.localPosition, scale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI);
        }

        static public NDTweenWorker RotateTo( GameObject target, float timeInSeconds, Vector3 rotation, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {
            
            return NDTween.To( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker RotateTo( GameObject target, float timeInSeconds, Vector3 rotation, NDTweenOptions options ) {
            
            return NDTween.To( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI);
        }

        static public NDTweenWorker ColorTo( GameObject target, float timeInSeconds, Color color, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.To( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, target.transform.localRotation.eulerAngles, color, "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker ColorTo( GameObject target, float timeInSeconds, Color color, NDTweenOptions options ) {

            return NDTween.To( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, target.transform.localRotation.eulerAngles, color, "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI );
        }

        static public NDTweenWorker ColorTo( GameObject target, float timeInSeconds, Color color, string colorTarget, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.To( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, target.transform.localRotation.eulerAngles, color, colorTarget, easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI );
        }

        static public NDTweenWorker ColorTo( GameObject target, float timeInSeconds, Color color, string colorTarget, NDTweenOptions options ) {

            return NDTween.To( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, target.transform.localRotation.eulerAngles, color, colorTarget, options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.To( target, timeInSeconds, position, scale, rotation, color, "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, NDTweenOptions options ) {

            return NDTween.To( target, timeInSeconds, position, scale, rotation, color, "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, string colorTarget, NDTweenOptions options ) {

            return NDTween.To( target, timeInSeconds, position, scale, rotation, color, colorTarget, options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI );
        }

        static public NDTweenWorker UITo( GameObject target, float timeInSeconds, Vector3 position, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true ) {

            return NDTween.To( target, timeInSeconds, position );
        }

        static public NDTweenWorker To( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, string colorTarget, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.CreateTweenWorker(
                true,
                target,
                position,
                scale,
                rotation,
                color,
                colorTarget,
                timeInSeconds,
                easing,
                delay,
                destroyOnComplete,
                clearCurrentTweens,
                autoPlay,
                isUI
            );
        }

        /*
        	Tween a game object FROM the supplied values TO it's current state
        */

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector3 position, NDTweenOptions options ) {

            return NDTween.From( target, timeInSeconds, position, target.transform.localScale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI);
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector3 position, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.From( target, timeInSeconds, position, target.transform.localScale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, NDTweenOptions options ) {

            return NDTween.From( target, timeInSeconds, position, scale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI);
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.From( target, timeInSeconds, position, scale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, NDTweenOptions options ) {

            return NDTween.From( target, timeInSeconds, position, scale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI);
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.From( target, timeInSeconds, position, scale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.From( target, timeInSeconds, position, scale, rotation, color, "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, NDTweenOptions options ) {

            return NDTween.From( target, timeInSeconds, position, scale, rotation, color, "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI);
        }

        static public NDTweenWorker ScaleFrom( GameObject target, float timeInSeconds, Vector3 scale, NDTweenOptions options ) {
            
            return NDTween.From( target, timeInSeconds, target.transform.localPosition, scale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI);
        }

        static public NDTweenWorker ScaleFrom( GameObject target, float timeInSeconds, Vector3 scale, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {
            
            return NDTween.From( target, timeInSeconds, target.transform.localPosition, scale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker RotateFrom( GameObject target, float timeInSeconds, Vector3 rotation, NDTweenOptions options ) {
            
            return NDTween.From( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI);
        }

        static public NDTweenWorker RotateFrom( GameObject target, float timeInSeconds, Vector3 rotation, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {
            
            return NDTween.From( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI);
        }

        static public NDTweenWorker ColorFrom( GameObject target, float timeInSeconds, Color color, NDTweenOptions options ) {

            return NDTween.From( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, target.transform.localRotation.eulerAngles, color, "_Color", options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI );
        }

        static public NDTweenWorker ColorFrom( GameObject target, float timeInSeconds, Color color, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.From( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, target.transform.localRotation.eulerAngles, color, "_Color", easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI );
        }

        static public NDTweenWorker ColorFrom( GameObject target, float timeInSeconds, Color color, string colorTarget, NDTweenOptions options ) {

            return NDTween.From( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, target.transform.localRotation.eulerAngles, color, colorTarget, options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI );
        }

        static public NDTweenWorker ColorFrom( GameObject target, float timeInSeconds, Color color, string colorTarget, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.From( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, target.transform.localRotation.eulerAngles, color, colorTarget, easing, delay, destroyOnComplete, clearCurrentTweens, autoPlay, isUI );
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, string colorTarget, NDTweenOptions options ) {

            return NDTween.From( target, timeInSeconds, target.transform.localPosition, target.transform.localScale, target.transform.localRotation.eulerAngles, color, colorTarget, options.easing, options.delay, options.destroyOnComplete, options.clearCurrentTweens, options.autoPlay, options.isUI );
        }

        static public NDTweenWorker From( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, string colorTarget, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = true, bool autoPlay = true, bool isUI = false ) {

            return NDTween.CreateTweenWorker(
                false,
                target,
                position,
                scale,
                rotation,
                color,
                colorTarget,
                timeInSeconds,
                easing,
                delay,
                destroyOnComplete,
                clearCurrentTweens,
                autoPlay,
                isUI
            );
        }


        /**
            Strips any previous NDTweenWorker components from the target GameObject
            @param GameObject target - the object to remove tweens from
            @param bool jumpToEndState - (default: false) Should the tween move to it's completed state
        */
        static public void RemoveAllTweens( GameObject target, bool jumpToEndState = false ) {

            NDTweenWorker[] workers = target.GetComponents<NDTweenWorker>();
            for(int i = 0; i < workers.Length; i++) {
                if( jumpToEndState ) workers[i].JumpToEndState();
                GameObject.Destroy( workers[i] );
            }

        }

        /*
            Staggers the firing of tweens in the provided array by the specified delay
        */
        static public void Stagger( NDTweenWorker[] tweens, float delay ) {

            NDTweenWorker tween;
            for(int i = 0; i < tweens.Length; i++)
            {
                tween = tweens[i];
                float newDelay = tween.delay + (delay * i);
                if(tween.isTo) NDTween.To( tween.targetGameObject, tween.tweenTimeInSeconds, tween.endPosition, tween.endScale, tween.endRotation, tween.endColor, tween.colorTarget, tween.easingEquation, newDelay, tween.destroyOnComplete, tween.clearCurrentTweens, true, tween.isUI );
                else NDTween.From( tween.targetGameObject, tween.tweenTimeInSeconds, tween.startPosition, tween.startScale, tween.startRotation, tween.startColor, tween.colorTarget, tween.easingEquation, newDelay, tween.destroyOnComplete, tween.clearCurrentTweens, true, tween.isUI );
            }

        }

        /*
            Staggers the firing of timelines in the provided array by the specified delay
        */
        static public void Stagger( NDTweenTimeline[] tweens, float delay ) {

            for(int i = 0; i < tweens.Length; i++)
            {
                tweens[i].Play( delay*i );
            }

        }

        static public Color GetMaterialColor( GameObject target, string propertyName ) {
            Renderer renderer = target.GetComponent<Renderer>();
            if(renderer == null) return new Color(0f, 0f, 0f, 0f);
            if(!renderer.sharedMaterial.HasProperty("_Color")) return new Color(0f, 0f, 0f, 0f);
            else return renderer.sharedMaterial.GetColor( propertyName );
        }


        /*
        =====
        Internal Methods
        =====
        */ 

        /**
            Returns the appropriate easing function from Easing.cs
        */
        static internal Func<float, float> GetEasingEquation( Func<float, float> easing ) {
            return easing == null ? Easing.none : easing;
        }

        

        /*
        =====
        Private Methods
        =====
        */ 

        /**
            Creates an NDTweenWorker component on the target game object
        */
        static private NDTweenWorker CreateTweenWorker( bool isTo, GameObject target, Vector3 position, Vector3 scale, Vector3 rotation, Color color, string colorTarget, float timeInSeconds, Func<float, float> easing = null, float delay = 0f, bool destroyOnComplete = true, bool clearCurrentTweens = false, bool autoPlay = true, bool isUI = false, bool isGlobal = false ) {

            //remove any previous tweens if needed
            if(clearCurrentTweens) NDTween.RemoveAllTweens( target );
            
            //create the component and add to the target game object
            NDTweenWorker tweenWorker = target.AddComponent<NDTweenWorker>();
            tweenWorker.targetGameObject = target;

            //set tween properties
            tweenWorker.startPosition = isTo ? isGlobal ? target.transform.position : target.transform.localPosition : position;
            tweenWorker.endPosition = isTo ? position : isGlobal ? target.transform.position : target.transform.localPosition;

            tweenWorker.startScale = isTo ? target.transform.localScale : scale;
            tweenWorker.endScale = isTo ? scale : target.transform.localScale;

            tweenWorker.startRotation = isTo ? target.transform.localRotation.eulerAngles : rotation;
            tweenWorker.endRotation = isTo ? rotation : target.transform.localRotation.eulerAngles;

            tweenWorker.startColor = isTo ? NDTween.GetMaterialColor( target, colorTarget ) : color;
            tweenWorker.endColor = isTo ? color : NDTween.GetMaterialColor( target, colorTarget );

            tweenWorker.colorTarget = colorTarget;

            tweenWorker.tweenTimeInSeconds = timeInSeconds;
            tweenWorker.delay = delay;

            tweenWorker.isTo = isTo;

            tweenWorker.isUI = isUI;

            tweenWorker.easingEquation = NDTween.GetEasingEquation( easing );

            tweenWorker.destroyOnComplete = destroyOnComplete;

            //start it
            if( autoPlay) tweenWorker.StartTween();

            //return reference
            return tweenWorker;

        }

        


    }
}
