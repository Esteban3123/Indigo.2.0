'************************************************************
' Assembly         : Domain.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/01/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IQuotationRepository
    Inherits IRepository(Of Quotation)

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetQuotation(Code As String) As Quotation

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetQuotationById(Id As Integer) As Quotation

    ''' <summary>
    ''' Proceso de cotización
    ''' </summary>
    ''' <param name="xmlData"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_SaveQuotation(xmlData As String, codeUser As String) As SP_SaveQuotation_Result

End Interface
