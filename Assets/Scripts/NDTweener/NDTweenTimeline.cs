using System;
using System.Collections.Generic;
using UnityEngine;

namespace NDTweener
{
    public class NDTweenTimeline {

        // events + delegates
        public delegate void NDTimelineStart();
        public delegate void NDTimelineProgress( float progress );
        public delegate void NDTimelineComplete();
        
        public event NDTimelineStart OnTimelineStart;
        public event NDTimelineProgress OnTimelineProgress;
        public event NDTimelineComplete OnTimelineComplete;


        // List of all steps involved in the Timeline animation
        private List<NDTweenTimelineStep> tweens;

        // List index of current place in Timeline
        private int currentTween = 0;

        // Currently active tween
        private NDTweenWorker activeTween = null;


        // Current tween progress
        private float currentTweenProgress = 0f;
        // Summed total of all tween lengths + delays (in seconds)
        private float totalTweenTime = 0f;


        /*
        =====
        Constructor
        =====
        */
        public NDTweenTimeline() {

            tweens = new List<NDTweenTimelineStep>();
        }

        /*
        =====
        Public API
        =====
        */


        /*
            Start the timeline animation
        */
        public void Play( float delay = 0f ) {

            currentTween = 0;

            CalculateStepPercentages();

            StartNextTween( delay );
            
        }

        /*
            Returns total progress for the Timeline (sum of all tweens' length)
        */
        public float GetOverallProgress() {
            float progress = 0f;

            //loop through completed tweens and add to progress
            for(int i = 0; i < currentTween; i++)
            {
                progress += tweens[i].overallTweenPercentage;
            }

            //then add on the current tweens progress * its overallTweenPercentage
            if( currentTween < tweens.Count )
            {
                progress += currentTweenProgress * tweens[currentTween].overallTweenPercentage;
            }

            return progress;
        }

        /*
            add an NDTween.To step to the tweens array
        */

        public void AddTo( GameObject target, float timeInSeconds, Vector3 position, Func<float, float> easing = null, float delay = 0f, bool isUI = false ) {

            AddTo(target, timeInSeconds, position, target.transform.localScale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, isUI);
        }
        public void AddTo( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Func<float, float> easing = null, float delay = 0f, bool isUI = false ) {

            AddTo(target, timeInSeconds, position, scale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, isUI);
        }
        public void AddTo( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Func<float, float> easing = null, float delay = 0f, bool isUI = false ) {

            AddTo(target, timeInSeconds, position, scale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, isUI);
        }
        public void AddTo( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, Func<float, float> easing = null, float delay = 0f, bool isUI = false ) {

            AddTo(target, timeInSeconds, position, scale, rotation, color, "_Color", easing, delay, isUI);
        }

        public void AddTo( GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, string colorTarget, Func<float, float> easing = null, float delay = 0f, bool isUI = false ) {

            NDTweenTimelineStep step = new NDTweenTimelineStep();
            step.isTo = true;
            step.target = target;
            step.position = position;
            step.scale = scale;
            step.rotation = rotation;
            step.color = color;
            step.colorTarget = colorTarget;
            step.timeInSeconds = timeInSeconds;
            step.easing = easing;
            step.delay = delay;
            step.isUi = isUI;

            totalTweenTime += (timeInSeconds + delay);

            tweens.Add( step );

        }

        /*
            add an NDTween.From step to the tweens array
        */
        public void AddFrom(GameObject target, float timeInSeconds, Vector3 position, Func<float, float> easing = null, float delay = 0f, bool isUI = false ) {

            AddFrom(target, timeInSeconds, position, target.transform.localScale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, isUI);
        }
        public void AddFrom(GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Func<float, float> easing = null, float delay = 0f, bool isUI = false ) {

            AddFrom(target, timeInSeconds, position, scale, target.transform.localRotation.eulerAngles, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, isUI);
        }
        public void AddFrom(GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Func<float, float> easing = null, float delay = 0f, bool isUI = false ) {

            AddFrom(target, timeInSeconds, position, scale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, isUI);
        }
        public void AddFrom(GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, Func<float, float> easing = null, float delay = 0f, bool isUI = false ) {

            AddFrom(target, timeInSeconds, position, scale, rotation, NDTween.GetMaterialColor(target, "_Color"), "_Color", easing, delay, isUI);
        }

        public void AddFrom(GameObject target, float timeInSeconds, Vector3 position, Vector3 scale, Vector3 rotation, Color color, string colorTarget, Func<float, float> easing = null, float delay = 0f, bool isUI = false ) {

            NDTweenTimelineStep step = new NDTweenTimelineStep();
            step.isTo = false;
            step.target = target;
            step.position = position;
            step.scale = scale;
            step.rotation = rotation;
            step.color = color;
            step.colorTarget = colorTarget;
            step.timeInSeconds = timeInSeconds;
            step.easing = easing;
            step.delay = delay;
            step.isUi = isUI;

            totalTweenTime += (timeInSeconds + delay);

            tweens.Add( step );

        }

        /*
        =====
        Private Methods
        =====
        */

        /*
            Starts the next tween in the tweens List
        */
        private void StartNextTween( float delay = 0f ) {
            
            //remove any listeners from previous tween
            if(activeTween != null) {
                activeTween.OnTweenProgress -= OnTweenProgress;
                activeTween.OnTweenComplete -= OnTweenComplete;
            }

            //grab the next tween step
            NDTweenTimelineStep step = (NDTweenTimelineStep) tweens[currentTween];

            // start the tweem
            if(step.isTo) {

                activeTween = NDTween.To(
                    step.target,
                    step.timeInSeconds,
                    step.position,
                    step.scale,
                    step.rotation,
                    step.color,
                    step.colorTarget,
                    step.easing,
                    step.delay + delay,
                    true,
                    true,
                    true,
                    step.isUi
                );
            } 
            else
            {
                activeTween = NDTween.From(
                    step.target,
                    step.timeInSeconds,
                    step.position,
                    step.scale,
                    step.rotation,
                    step.color,
                    step.colorTarget,
                    step.easing,
                    step.delay + delay,
                    true,
                    true,
                    true,
                    step.isUi
                );
            }

            // listen to tween events for new tween
            activeTween.OnTweenComplete += OnTweenComplete;
            activeTween.OnTweenProgress += OnTweenProgress;

            if( OnTimelineStart != null ) OnTimelineStart();
            
        }


        /*
            Calculate total percentage of each tween as part of total timeline
        */
        private void CalculateStepPercentages() {

            NDTweenTimelineStep step;
            for(int i = 0; i < tweens.Count; i++){
                step = (NDTweenTimelineStep) tweens[i];
                step.overallTweenPercentage = (step.timeInSeconds + step.delay) / totalTweenTime;
                tweens[i] = step;
            }

        }


        /*
            Track progress of current tween
        */
        private void OnTweenProgress( float progress ) {

            currentTweenProgress = progress;
            if( OnTimelineProgress != null ) OnTimelineProgress( GetOverallProgress() );
        }

        /*
            Current tween has completed - fire next tween or complete event
        */
        private void OnTweenComplete() {

            currentTween++;
            currentTweenProgress = 0f;
            if( currentTween < tweens.Count ) StartNextTween();
            else if( OnTimelineComplete != null ) OnTimelineComplete(); 

        }


        /*
        =====
        Internal Methods
        =====
        */

        //nothing to see here....
        

        /*
        =====
        Internal classes
        =====
        */

        /*
            Stores tween step data
        */
        internal struct NDTweenTimelineStep {

            public bool isTo;
            public GameObject target;
            public Vector3 position;
            public Vector3 scale;
            public Vector3 rotation;
            public Color color;
            public string colorTarget;
            public float timeInSeconds;
            public Func<float, float> easing;
            public float delay;
            public bool isUi;
            public float overallTweenPercentage; //populated on Play()

        }
    }
}