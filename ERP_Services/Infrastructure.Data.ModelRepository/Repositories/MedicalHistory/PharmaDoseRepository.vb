'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Duván Mejía Cortes
' Created          : 22/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class PharmaDoseRepository
    Inherits GenericRepository(Of PharmaDose)
    Implements IPharmaDoseRepository, Inject

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
