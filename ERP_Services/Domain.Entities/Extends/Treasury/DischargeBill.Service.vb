Imports System.Runtime.Serialization

Partial Public Class DischargeBill

#Region "New"
    <DataMember()>
    Property AccountPayableBillNumber As String
    <DataMember()>
    Property AccountPayableShareDateExpires As DateTime
    <DataMember()>
    Property AccountPayableShareBalance As Decimal
    <DataMember()>
    Property AccountPayableShareShare As Integer
    <DataMember()>
    Property OperativeUnitId As Integer
    <DataMember()>
    Property ColorAgePortFolio As Integer

    ''' <summary>
    ''' Id del concepto de egreso que tiene amarrado el detalle del comprobante de egreso para asi poder filtar y asignar al detalle adecuado en el guardar
    ''' </summary>
    ''' <value>
    ''' The payment concept identifier.
    ''' </value>
    <DataMember()> _
    Property ExpenseConceptId As Integer

#End Region

End Class
