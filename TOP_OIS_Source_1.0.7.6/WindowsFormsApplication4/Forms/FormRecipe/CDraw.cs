using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XModule.Forms.FormRecipe
{
    
    public static class CMath
    {
        public static double AngleFlipX(double dAngle)
        {
            double angle = 180.0 - dAngle;
            if (angle > 360.0)
                angle -= 360.0;
            if (angle < 0.0)
                angle = 360.0 + angle;
            return angle;
        }

        public static double AngleFlipY( double dAngle)
        {
            double angle = 360.0 - dAngle;
            if (angle > 360.0)
                angle -= 360.0;
            if (angle < 0.0)
                angle = 360.0 + angle;
            return angle;
        }
        public static double GetAngle(PointF Center, PointF Pos)
        {
            double y = Center.Y - Pos.Y;
            double x = Center.X - Pos.X;

            return Math.Atan2( y,x) * (180d / Math.PI); //인자 y,x =>x,y로 변경하여 테스트
        }
        /// <summary>
        /// 두 점 사이의 거리 값을 구한다
        /// </summary>
        /// <param name="ptStart"></param>
        /// <param name="ptEnd"></param>
        /// <returns></returns>
        public static double GetDistance(PointF ptStart, PointF ptEnd)
        {
            double dDist = 0.0;

            dDist = Math.Sqrt(
                                ((ptEnd.X - ptStart.X) * (ptEnd.X - ptStart.X)) +
                                ((ptEnd.Y - ptStart.Y) * (ptEnd.Y - ptStart.Y))
                             );

            return dDist;
        }
        /// <summary>
        /// 원에서 해당하는 각도의 위치를 가져온다
        /// </summary>
        /// <param name="dCenterX">중심점 X</param>
        /// <param name="dCenterY">중심점 Y</param>
        /// <param name="dRadius">반지름</param>
        /// <param name="dAngle">각도</param>
        /// <param name="dGetX">반환할 위치 값 X</param>
        /// <param name="dGetY">반환할 위치 값 Y</param>
        public static void GetPosCircle(double dCenterX, double dCenterY, double dRadius, double dAngle, ref double dGetX, ref double dGetY)
        {
            //dAngle = dAngle.ToRadians();

           double Radian = (dAngle / 180.0) *Math.PI;

            dGetX = dRadius * Math.Cos(Radian) - dCenterX;

            dGetY = dRadius * Math.Sin(Radian) - dCenterY;



        }

        public static float GetSweep(double startAngle, double endAngle)
        {
            float dSweep = 0.0f;

            if (startAngle > endAngle)
                dSweep = (float)(360.0 + endAngle - startAngle);
            else
                dSweep = (float)(endAngle - startAngle);

            return dSweep;
        }
    }
    public class CRect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;


        public int Width
        {
            get { return (Right - Left) > 0 ? Right - Left : Left - Right; }
        }

        public int Height
        {
            get { return (Top - Bottom) > 0 ? Top - Bottom : Bottom - Top; }
        }

        public CRect()
        {

        }

        public CRect(int left, int bottom, int right, int top)
        {
            this.Left = left;
            this.Bottom = bottom;
            this.Right = right;
            this.Top = top;
        }

        public void CopyTo(ref CRect rect)
        {
            if (rect == null)
                rect = new CRect();

            rect.Left = Left;
            rect.Bottom = Bottom;
            rect.Right = Right;
            rect.Top = Top;
        }

        public Rectangle ToRect()
        {
            return new Rectangle(Left, Top, Width, Height);
        }

        public override string ToString()
        {
            return string.Format("Left : {0} Bottom : {1} Right : {2} Top : {3}", Left, Bottom, Right, Top);
        }

        public void SwapX()
        {
            int nTemp = Left;
            Left = Right;
            Right = nTemp;
        }

        public void SwapY()
        {
            int nTemp = Bottom;
            Bottom = Top;
            Top = nTemp;
        }
    }
}
