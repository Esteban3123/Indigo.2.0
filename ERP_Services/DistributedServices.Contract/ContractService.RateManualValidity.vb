#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Contract
Imports Microsoft.Practices.Unity

#End Region

Partial Class ContractService

    ''' <summary>
    ''' Obtiene una vigencia de manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRateManualValidityById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of RateManualValidity) Implements IContractRateManualValidity.GetRateManualValidityById
        Using service As IRateManualValidityAdminService = Container.Current.Resolve(Of IRateManualValidityAdminService)()
            Return service.GetRateManualValidityById(id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una vigencia de manual tarifario por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetRateManualValidity(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of RateManualValidity) Implements IContractRateManualValidity.GetRateManualValidity
        Using service As IRateManualValidityAdminService = Container.Current.Resolve(Of IRateManualValidityAdminService)()
            Return service.GetRateManualValidity(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una vigencia de manual tarifario
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveRateManualValidity(ByVal rateManualValidity As RateManualValidity, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RateManualValidity) Implements IContractRateManualValidity.SaveRateManualValidity
        Using service As IRateManualValidityAdminService = Container.Current.Resolve(Of IRateManualValidityAdminService)()
            Return service.SaveRateManualValidity(rateManualValidity, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de la vigencia de manual tarifario
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateRateManualValidity(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of RateManualValidity) Implements IContractRateManualValidity.ChangeStateRateManualValidity
        Using service As IRateManualValidityAdminService = Container.Current.Resolve(Of IRateManualValidityAdminService)()
            Return service.ChangeStateRateManualValidity(code, state, audit)
        End Using
    End Function

End Class
