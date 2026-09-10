'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Andres Alarcon
' Created          : 24/10/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Base

Public Class ReasonscancellationNPTRepository
    Inherits GenericRepository(Of ReasonscancellationNPT)
    Implements IReasonscancellationNPTRepository, Inject


    ''' <summary>
    ''' Contexto de Package
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Package
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class