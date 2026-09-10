'***********************************************************************
' Assembly         : Infrastructure.Data.BatchSerialSequenceRepository
' Author           : Giovanny Plazas
' Created          : 24-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Repositorio de la entidad secuencia numerica para lotes
''' </summary>
Public Class BatchSerialSequenceRepository
    Inherits GenericRepository(Of BatchSerialSequence)
    Implements IBatchSerialSequenceRepository, Inject

#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub

#End Region

#Region "IBatchSerialSequenceRepository"



#End Region

End Class
