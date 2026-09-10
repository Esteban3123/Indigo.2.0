'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Yoe Andres Cardenas
' Created          : 15/04/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class NPTConfigurationRepository
    Inherits GenericRepository(Of NPTConfiguration)
    Implements INPTConfigurationRepository, Inject
    ''' <summary>
    ''' Contexto de Turn
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Turn
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' consulta todos los registros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllNPTConfiguration() As List(Of NPTConfiguration) Implements INPTConfigurationRepository.ListAllNPTConfiguration
        Dim query = From e In _context.NPTConfiguration.Include("InventoryProduct")
                    Select e
        Return query.ToList()
    End Function

End Class