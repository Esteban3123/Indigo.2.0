'***********************************************************************
' Assembly         : Infrastructure.Data.CareGroupRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 18/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SurgeriesPercentageManualRepository
    Inherits GenericRepository(Of SurgeriesPercentageManual)
    Implements ISurgeriesPercentageManualRepository


    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' metodo para obtener las tarifas para los eventos en la orden de servicio
    ''' </summary>
    ''' <param name="RateManualId"></param>
    ''' <param name="InterventionType"></param>
    ''' <returns></returns>
    Public Function GetSurgeriesPercentageManualByRateManualIdInterventionType(RateManualId As Integer, InterventionType As Integer) As SurgeriesPercentageManual Implements ISurgeriesPercentageManualRepository.GetSurgeriesPercentageManualByRateManualIdInterventionType
        Dim res = (From spm In _context.SurgeriesPercentageManual.Include("RateManual").AsNoTracking() Where spm.RateManualId = RateManualId And spm.InterventionType = InterventionType Select spm).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New SurgeriesPercentageManual
        End If
    End Function
End Class
