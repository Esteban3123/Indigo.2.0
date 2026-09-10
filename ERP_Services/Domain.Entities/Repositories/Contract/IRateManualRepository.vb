'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IRateManualRepository
    Inherits IRepository(Of RateManual)

    ''' <summary>
    ''' Obtiene un manual tarifario por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRateManual(code As String) As RateManual

    ''' <summary>
    ''' Obtiene un manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRateManualById(id As Integer, Optional tracking As Boolean = True) As RateManual

    ''' <summary>
    ''' Obtiene un manual tarifario con los agregados de rateManualDetail y rateManualDetailSurgical
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRateManualByIdWithAggregates(id As Integer) As RateManual

    ''' <summary>
    ''' Valida el CopyPaste del form de manual de tarifas
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteRateManual(xmlObject As String, ServiceManual As Integer, GridOption As Integer) As List(Of SP_CopyAndPasteRateManual_Result)

End Interface
