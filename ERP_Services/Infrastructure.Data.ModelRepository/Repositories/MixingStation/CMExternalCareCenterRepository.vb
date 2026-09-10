'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Diego A. Roldan
' Created          : 2022-02-14
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class CMExternalCareCenterRepository
    Inherits GenericRepository(Of CMExternalCareCenter)
    Implements ICMExternalCareCenterRepository, Inject

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
