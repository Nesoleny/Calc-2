namespace EveryDay.Calc.Calculation.Models
{
    public class DivOperation : Operation
    {
        public override string Name
        {
            get { return "Div"; }
        }

        public override double? GetResult()
        {
            Error = "";
            if (Input.Length != 2)
            {
                Error = "Введите нормальные данные: два числа!";
                return null;
            }

            return Input[0] / Input [1];
        }
    }
}
