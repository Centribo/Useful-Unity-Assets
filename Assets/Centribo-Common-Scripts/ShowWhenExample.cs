using System;
using UnityEngine;

public class ShowWhenExample : MonoBehaviour
{
    #region Bool Region

    [Header("Check with BOOL")]
    public bool show;
    
    [ShowWhen("show")]
    public float numberWhenTrue;

    [ShowWhen("show", false)]
    public Vector3 vector3WhenFalse;

    [ShowWhen("show")]
    public float[] arrayWhenTrue = {1, 2, 3};

    [Serializable]
    public class ArrayClass
    {
        public Color[] colorArray = {Color.red, Color.blue, Color.green};
    }

    [ShowWhen("show")]
    public ArrayClass workaroundForArrayWhenTrue;

    [Serializable]
    public class CustomClass
    {
        public float floatValue = 99;
        public string stringValue = "string";
    }

    [ShowWhen("show", true)]
    public CustomClass customClass;
  
    [ShowWhen("show", "error")]
    public string stringErrorComparationValueType;

    #endregion

    #region Enum Region

    public enum EaseType
    {
        Linear,
        OutQuad,
        InOutQuint
    }
    
    public enum OtherEnum
    {
        Enum1
    }
    
    [Space(20), Header("Check with ENUM")]
    public EaseType easeType;
    
    [ShowWhen("easeType", EaseType.Linear)]
    public string stringWhenLinear = "Linear";
    
    [ShowWhen("easeType", new object[]{EaseType.Linear, EaseType.OutQuad})]
    public string stringWhenLinearAndOutQuad = "LinearAndOutQuad";
    
    [ShowWhen("easeType", EaseType.InOutQuint)]
    public string stringWhenInOutQuint = "InOutQuint";
    
    [ShowWhen("easeType")]
    public string stringErrorNeedParam;
    
    [ShowWhen("easeType",5)]
    public string stringErrorNotEnum;

    public OtherEnum otherEnum;
    
    [ShowWhen("otherEnum", new object[]{OtherEnum.Enum1, EaseType.Linear})]
    public string stringErrorWrongEnumType;

    #endregion

    #region Int Region

    [Space(20), Header("Check with INT")]
    public int intValue;
    
    [ShowWhen("intValue", ">5")]
    public string stringWhenGreaterThan5 = "Greater Than 5";
    
    [ShowWhen("intValue", ">3+5")]
    public string stringErrorNotKnownOperation;

    #endregion

    #region Float Region

    [Space(20), Header("Check with FLOAT")]
    public float floatValue;

    [ShowWhen("floatValue", "!=2")]
    public GameObject showWhenOtherThan2;
    
    [ShowWhen("floatValueError", ">5")]
    public string stringErrorParameterNotKnown;
    
    [ShowWhen("stringErrorParameterNotKnown", ">+5")]
    public string stringErrorTypeNotSupported;

    #endregion

}