using System.IO;
using System.IO.Ports;
using System.Threading;
using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Fusion;
using System.Windows.Media.Media3D;

namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    class Pose
    {

        //Arms
        public double AngleRightElbow;
        public double AngleRightShoulder;
        public double AngleRightWrist;
        public double AngleLeftElbow;
        public double AngleLeftShoulder;
        public double AngleLeftWrist;

        //Flags
        public string speechString = "";

        bool rightShoulderStraight;
        bool leftShoulderStraight;
        bool rightArmStraight;
        bool leftArmStraight;
        bool rightWristStraight;
        bool leftWristStraight;



        public Pose() {
            rightShoulderStraight = false;
            leftShoulderStraight = false;
            rightArmStraight = false;
            leftArmStraight = false;
            rightWristStraight = false;
            leftWristStraight = false;
        }        

        /// <summary>
        /// Calculates the body angle corresponding to the opposing side of the triangle using the law of cosines (in degrees)
        /// </summary>
        /// <param name="opp">Opposing side of the triangle</param>
        /// <param name="adj1">One adjacent side to angle</param>
        /// <param name="adj2">Other adjacent side to angle</param>
        public double calcAngle(Vector3D vectorA, Vector3D vectorB)
        {
            double dotProduct;
            vectorA.Normalize();
            vectorB.Normalize();
            dotProduct = Vector3D.DotProduct(vectorA, vectorB);

            return (double)Math.Acos(dotProduct) / Math.PI * 180;
        }

        public void GetVector(Skeleton skeleton)
        {
            Vector3D ShoulderCenter = new Vector3D(skeleton.Joints[JointType.ShoulderCenter].Position.X, skeleton.Joints[JointType.ShoulderCenter].Position.Y, skeleton.Joints[JointType.ShoulderCenter].Position.Z);
            Vector3D RightShoulder = new Vector3D(skeleton.Joints[JointType.ShoulderRight].Position.X, skeleton.Joints[JointType.ShoulderRight].Position.Y, skeleton.Joints[JointType.ShoulderRight].Position.Z);
            Vector3D LeftShoulder = new Vector3D(skeleton.Joints[JointType.ShoulderLeft].Position.X, skeleton.Joints[JointType.ShoulderLeft].Position.Y, skeleton.Joints[JointType.ShoulderLeft].Position.Z);
            Vector3D RightElbow = new Vector3D(skeleton.Joints[JointType.ElbowRight].Position.X, skeleton.Joints[JointType.ElbowRight].Position.Y, skeleton.Joints[JointType.ElbowRight].Position.Z);
            Vector3D LeftElbow = new Vector3D(skeleton.Joints[JointType.ElbowLeft].Position.X, skeleton.Joints[JointType.ElbowLeft].Position.Y, skeleton.Joints[JointType.ElbowLeft].Position.Z);
            Vector3D RightWrist = new Vector3D(skeleton.Joints[JointType.WristRight].Position.X, skeleton.Joints[JointType.WristRight].Position.Y, skeleton.Joints[JointType.WristRight].Position.Z);
            Vector3D LeftWrist = new Vector3D(skeleton.Joints[JointType.WristLeft].Position.X, skeleton.Joints[JointType.WristLeft].Position.Y, skeleton.Joints[JointType.WristLeft].Position.Z);
            Vector3D RightHand = new Vector3D(skeleton.Joints[JointType.HandRight].Position.X, skeleton.Joints[JointType.HandRight].Position.Y, skeleton.Joints[JointType.HandRight].Position.Z);
            Vector3D LeftHand = new Vector3D(skeleton.Joints[JointType.HandLeft].Position.X, skeleton.Joints[JointType.HandLeft].Position.Y, skeleton.Joints[JointType.HandLeft].Position.Z);
            Vector3D YUpVector = new Vector3D(0.0, 1.0, 0.0);
            Vector3D XRightVector = new Vector3D(1.0, 0.0, 0.0);
            Vector3D ZBackVector = new Vector3D(0.0, 0.0, 1.0);

            AngleRightElbow = calcAngle(RightElbow - RightShoulder, RightElbow - RightWrist);
            AngleRightShoulder = calcAngle(YUpVector, RightShoulder-RightElbow);
            AngleRightWrist = calcAngle(RightElbow - RightWrist, RightWrist - RightHand);
            AngleLeftElbow = calcAngle(LeftElbow - LeftShoulder, LeftElbow - LeftWrist);
            AngleLeftShoulder = calcAngle(YUpVector, LeftShoulder-LeftElbow);
            AngleLeftWrist = calcAngle(LeftElbow - LeftWrist, LeftWrist - LeftHand);



        
        }

        public void checkPose(Skeleton skeleton) {

            //Initial insructions


            //Check alignment of arms
            speechString = "straighten both your arms to your side, pointing outwards7";



            //Right arm flag setting
            if (AngleRightShoulder > 80 && AngleRightShoulder < 100)
            {
                rightShoulderStraight = true;
            }
            else { rightShoulderStraight = false; }

            if (AngleRightElbow > 170 && AngleRightElbow < 190)
            {
                rightArmStraight = true;
            }

            else {
                rightArmStraight = false;
            }

            if (AngleRightWrist < 15)
            {
                rightWristStraight = true;
            }
            else {
                rightWristStraight = false;
            }

            //Left arm flag setting
            if (AngleLeftShoulder > 80 && AngleLeftShoulder < 100)
            {
                leftShoulderStraight = true;
            }
            else { leftShoulderStraight = false; }

            if (AngleLeftElbow > 170 && AngleLeftElbow < 190)
            {
                leftArmStraight = true;
            }

            else
            {
                leftArmStraight = false;
            }

            if (AngleLeftWrist < 15)
            {
                leftWristStraight = true;
            }
            else
            {
                leftWristStraight = false;
            }

            //Checking conditions
            if (!rightShoulderStraight) { speechString = "extend your right arm to 3 o clock"; }
            else if (!leftShoulderStraight) { speechString = "extend your left arm to 9 o clock"; }
            else if (!rightArmStraight) { speechString = "straighten your right arm";  }
            else if (!leftArmStraight) { speechString = "straighten your left arm";  }
            else { speechString = "your arms are perfect!"; } 


        }


}

}