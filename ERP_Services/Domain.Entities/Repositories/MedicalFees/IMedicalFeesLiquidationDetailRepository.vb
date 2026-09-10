'************************************************************
' Assembly         : Domain.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IMedicalFeesLiquidationDetailRepository
    Inherits IRepository(Of MedicalFeesLiquidationDetail)

    ''' <summary>
    ''' Obtiene el listado de detalles de liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListMedicalFeesLiquidationDetailByMedicalFeesCausationId(MedicalFeesCausationId As Integer, Optional tracking As Boolean = True) As List(Of MedicalFeesLiquidationDetail)

    ''' <summary>
    ''' Obtiene el listado de los detalles de liquidacion de honorarios siempre y cuando este en estado anulado
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListMedicalFeesLiquidationDetailAnnular(MedicalFeesCausationId As Integer) As List(Of MedicalFeesLiquidationDetail)

    ''' <summary>
    ''' Valida que las causaciones esten o no en una liquidacion confirmada o registrada
    ''' </summary>
    ''' <param name="ListInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateMedicalFeesCausationIdContainInMFLDConfirmedOrRegister(ListInfo As List(Of Tuple(Of Integer, Integer))) As List(Of MedicalFeesLiquidationDetail)

    ''' <summary>
    ''' Valida que las causaciones esten o no en una liquidacion confirmada o registrada
    ''' </summary>
    ''' <param name="ListInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateMedicalFeesCausationIdContainInMFLDAnnular(ListInfo As List(Of Tuple(Of Integer, Integer))) As List(Of MedicalFeesLiquidationDetail)

End Interface
