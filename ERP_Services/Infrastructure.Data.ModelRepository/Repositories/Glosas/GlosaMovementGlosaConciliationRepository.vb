#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class GlosaMovementGlosaConciliationRepository
	Inherits GenericRepository(Of GlosaMovementGlosaConciliation)
	Implements IGlosaMovementGlosaConciliationRepository, Inject

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
