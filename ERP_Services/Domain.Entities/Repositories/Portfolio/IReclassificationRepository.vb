'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Rafael Eduardo Patiño cabrera
' Created          : 09-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IReclassificationRepository
    Inherits IRepository(Of PortfolioReclassification)

    ''' <summary>
    ''' metodo para obtener una reclasificacion de documento
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetReclassification(ByVal code As String, Optional tracking As Boolean = True) As PortfolioReclassification

    ''' <summary>
    ''' Crea un pago parcial glosas con un store procedure y enviando el partialpayment como Xml
    ''' </summary>
    ''' <param name="PartialPaymentsXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePartialPayments(PartialPaymentsXml As String, codeUser As String) As SP_GeneratePartialPayments_Result

End Interface
