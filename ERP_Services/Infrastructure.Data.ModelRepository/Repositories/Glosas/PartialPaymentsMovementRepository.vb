'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio pagos parcilaes en conciliacion
''' </summary>
Public Class PartialPaymentsMovementRepository
    Inherits GenericRepository(Of PartialPaymentsMovement)
    Implements IPartialPaymentsMovementRepository

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
