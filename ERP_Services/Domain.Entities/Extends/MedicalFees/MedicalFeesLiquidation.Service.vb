Imports System.Runtime.Serialization
Partial Public Class MedicalFeesLiquidation

    ''' <summary>
    ''' Obtiene o establece la descripcion del contrato profesional de la salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property MedicalFeesContractDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property FilingUnitDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property SupplierTypeDescription As String

    ''' <summary>
    ''' Obtiene o establece el codigo y el nombre del medico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property HealthProfessionalDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CostCenterDescription As String

End Class
