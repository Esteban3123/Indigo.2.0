'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Diego A. Roldán
' Created          : 2022-04-04
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

''' <summary>
''' Repositorio Cabeceras Conciliación
''' </summary>
Public Class ConceptGlosaRepository
    Inherits GenericRepository(Of ConceptGlosas)
    Implements IConceptGlosaRepository, Inject

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

End Class
