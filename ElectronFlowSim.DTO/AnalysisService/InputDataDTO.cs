namespace ElectronFlowSim.DTO.AnalysisService;

public class InputDataDTO
{
    /// <summary>
    /// число границ пушки
    /// </summary>
    public required int ig {  get; set; }

    /// <summary>
    /// число эквипотенциалей
    /// </summary>
    public required int nmas { get; set; }

    /// <summary>
    /// число задаваемых значений продольной составляющей
    /// магнитного поля на оси симметрии пушки
    /// </summary>
    public required int km { get; set; }

    /// <summary>
    /// число ячеек параболической сетки потенциалов в поперечном направлении
    /// </summary>
    public required int kp { get; set; }

    /// <summary>
    /// число ячеек сетки потенциалов в продольном направлении
    /// </summary>
    public required int kq { get; set; }

    /// <summary>
    /// число поперечных ячеек (предельное)
    /// </summary>
    public required int kpj6 { get; set; }

    /// <summary>
    /// номер катода (всегда 1. Когда пушки с полыми катода, то другое)
    /// </summary>
    public required int ik { get; set; }

    ///// <summary>
    ///// число основных электронов односкоростного пучка (нормальные к поверхности катода)
    ///// </summary>
    //public required int nl { get; set; }

    /// <summary>
    /// параметр, определяющий число дополнительных электронов для многоскоростного пучка
    /// </summary>
    public required int j1 { get; set; }

    /// <summary>
    /// номер первой итерации
    /// </summary>
    public required int icr { get; set; }

    /// <summary>
    /// количество итераций
    /// </summary>
    public required int jcr { get; set; }

    ///// <summary>
    ///// расчет в режиме 3/2 (=0)
    ///// расчет в режиме заданного тока (=2) - ток в мА
    ///// ток в машинных единицах (=3)
    ///// </summary>
    //public required int kl1 { get; set; }
    ///// <summary>
    ///// не считать траектории (=1)
    ///// не рисовать сетку потенциалов (=3)
    ///// </summary>
    //public required int kl2 { get; set; }
    ///// <summary>
    ///// не печатать значения потенциала в узлах сетки (=1)
    ///// </summary>
    //public required int kl3 { get; set; }
    ///// <summary>
    ///// управление записью рисунков на магнитный диск
    ///// запись (=1)
    ///// нет (=0)
    ///// </summary>
    //public required int kl4 { get; set; }
    //public required int kl5 { get; set; }
    //public required int kl6 { get; set; }

    ///// <summary>
    ///// 
    ///// </summary>
    //public required double[] rbr { get; set; }

    /// <summary>
    /// значения точек
    /// радиальная координата по оси ординат
    /// </summary>
    public required double[] r { get; set; }

    /// <summary>
    /// значения точек
    /// продольная координата по оси абсцисс
    /// </summary>
    public required double[] z { get; set; }
    
    /// <summary>
    /// массив потенциалов в узловых точках (нормирован)
    /// </summary>
    public required double[] u { get; set; }

    /// <summary>
    /// количество интервалов, на которые разбит контур
    /// </summary>
    public required int[] l { get; set; }

    /// <summary>
    /// радиус кривизны катода
    /// </summary>
    public required double rk { get; set; }

    /// <summary>
    /// наибольшее значение поперечной тепловой скорости
    /// </summary>
    public required double utep { get; set; }

    /// <summary>
    /// максимальная координата (постоянная)
    /// конец счета траектории
    /// </summary>
    public required double zkon { get; set; }

    ///// <summary>
    ///// коэффициент ослабления величины тока из-за его экранировки теневой сеткой
    ///// </summary>
    //public required double fekr { get; set; }

    /// <summary>
    /// радиус пролетного канала
    /// </summary>
    public required double akl1 { get; set; }

    /// <summary>
    /// нормирующий потенциал
    /// </summary>
    public required double u0 { get; set; }

    /// <summary>
    /// массив эквипотенциалов
    /// </summary>
    public required double[] uekvip { get; set; }

    /// <summary>
    /// нормирующее магнитное поле
    /// </summary>
    public required double bnorm { get; set; }

    /// <summary>
    /// кэоффициент корректировки магнитного поля
    /// </summary>
    public required double abm { get; set; }

    /// <summary>
    /// массив значений магнитных полей
    /// </summary>
    public required double[] bm { get; set; }

    /// <summary>
    /// массив значений тока
    /// режим заданного тока
    /// </summary>
    public required double[] aik { get; set; }

    /// <summary>
    /// шаг интегрирования по времени
    /// </summary>
    public required double ht { get; set; }

    /// <summary>
    /// расстояние от точек старта до поверхности
    /// </summary>
    public required double dz { get; set; }

    /// <summary>
    /// расстояние от катода до поверхности, где
    /// ток считается по закону 3/2
    /// </summary>
    public required double dtok { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public required double hq1 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public required double ar1s { get; set; }
}