'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/02/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SettingsContractRepository
    Inherits GenericRepository(Of SettingsContract)
    Implements ISettingsContractRepository

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
    ''' Obtiene los parámetros por unidad operativa
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    Public Function GetSettingsContractByOperatingUnitId(operatingUnitId As Integer, Optional tracker As Boolean = True) As SettingsContract Implements ISettingsContractRepository.GetSettingsContractByOperatingUnitId
        If operatingUnitId = 0 Then
            Throw New ArgumentNullException("operatingUnitId")
        End If
        Dim res As SettingsContract
        If tracker Then
            res = (From d In Me._context.SettingsContract.Include("SettingContractCareGroupType") Where d.OperatingUnitId = operatingUnitId Select d).FirstOrDefault
        Else
            res = (From d In Me._context.SettingsContract.AsNoTracking().Include("SettingContractCareGroupType") Where d.OperatingUnitId = operatingUnitId Select d).FirstOrDefault
        End If
        If res IsNot Nothing Then
            res.OriginalValue = (From d As SettingsContract In Me._context.SettingsContract.AsNoTracking Where d.OperatingUnitId = operatingUnitId Select d).FirstOrDefault
            Return res
        Else
            Return New SettingsContract()
        End If
    End Function

    ''' <summary>
    ''' Valida que si se cambia el parametro de descripciones a no no haya items registrados en los cups en estado activo
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidationDescriptions() As Integer Implements ISettingsContractRepository.ValidationDescriptions
        Return (From d In _context.CUPSEntityContractDescriptions.AsNoTracking() Where d.IsDelete = 0 Select d).Count()
    End Function

End Class
