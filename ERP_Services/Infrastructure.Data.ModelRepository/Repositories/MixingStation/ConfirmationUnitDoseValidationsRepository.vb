'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Andres Alarcon
' Created          : 2024-12-18
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class ConfirmationUnitDoseValidationsRepository
    Inherits GenericRepository(Of ConfirmationUnitDoseValidations)
    Implements IConfirmationUnitDoseValidationsRepository, Inject

    ''' <summary>
    ''' Contexto de Configuración de Central de Mezclas
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork
    ''' <summary>
    ''' Inicia el contexto de Configuración de Central de Mezclas
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub


End Class
