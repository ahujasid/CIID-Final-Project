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
       
        public Pose() { }        

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

        public double[] GetVector(Skeleton skeleton)
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

            double AngleRightElbow = calcAngle(RightElbow - RightShoulder, RightElbow - RightWrist);
            double AngleRightShoulder = calcAngle(YUpVector, RightShoulder-RightElbow);
            double AngleRightWrist = calcAngle(RightElbow - RightWrist, RightWrist - RightHand);
            double AngleLeftElbow = calcAngle(LeftElbow - LeftShoulder, LeftElbow - LeftWrist);
            double AngleLeftShoulder = calcAngle(YUpVector, LeftShoulder-LeftElbow);
            double AngleLeftWrist = calcAngle(LeftElbow - LeftWrist, LeftWrist - LeftHand);



            double[] Angles = { AngleRightShoulder, AngleRightElbow, AngleRightWrist, AngleLeftShoulder, AngleLeftElbow, AngleLeftWrist };
            return Angles;
        }

        protected void checkPose(Skeleton skeleton) {

            //Initial insructions

            
            //Check alignment of arms

        }


}

}