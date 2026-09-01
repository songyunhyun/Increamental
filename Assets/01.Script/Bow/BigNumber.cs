using System;
using UnityEngine;
[Serializable]
public struct BigNumber
{
    private double mantissa;
    private long exponent;
    public BigNumber(double mantissa, long exponent)
    {
        this.mantissa = mantissa;
        this.exponent = exponent;
    }
    public string Aaa()
    {
        double result = Math.Log(exponent, mantissa);
        string resultStr = result.ToString("F2");
        resultStr += $"e{mantissa}";
        return resultStr;
    }
}