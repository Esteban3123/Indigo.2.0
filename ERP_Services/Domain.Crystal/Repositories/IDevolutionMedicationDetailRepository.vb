'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Carlos Cordoba
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities


Public Interface IDevolutionMedicationDetailRepository
    Inherits IRepository(Of HCDEVMEDD)

    ''' <summary>
    ''' obtieene un detalle de la devolucion por consecutivo y por codigo de producto
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDevolutionMedicationDetailDetailByConsecutiveAndProductCode(consecutive As Integer, productCode As String) As HCDEVMEDD

    ''' <summary>
    ''' lista los detalles de la devolucion por el consecutivo y que tengan cantidad por entregar
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListDevolutionMedicationDetailDetailByConsecutive(consecutive As Integer) As List(Of HCDEVMEDD)
End Interface
