'***********************************************************************
' Assembly         : Presentacion.Accouting.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 20-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class PAccountingHomologations

#Region "Fields"
    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim _view As IAccountingHomologations

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim _indigo As SessionValues
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As IAccountingHomologations)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigo = SessionValues.Instance
            Me._view = view
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeLegalBook()
        _view.LegalBookXpo = XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListBookByStatusXpCollection(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeHomologationBook()
        _view.HomologationBookXpo = XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListBookAllByStatusAndOfficialBook(True, False)
    End Sub

	''' <summary>
	''' Lista los items que estan guardados en la BD
	''' </summary>
	''' <param name="LegalBookId"></param>
	''' <param name="HomologationBookId"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function ListViewHomologationAccountByLegalBookIdAndHomologationLegalBookId(LegalBookId As Integer, HomologationBookId As Integer) As XPCollection
		Return XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListViewHomologationAccountByLegalBookIdAndHomologationLegalBookId(LegalBookId, HomologationBookId)
	End Function

	''' <summary>
	''' Inicializa el datasource del search de libro oficial
	''' </summary>
	''' <remarks></remarks>
	Public Function InitializeRepositorySearch(LegalBookId As Integer) As XPCollection
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListAccountsByLegalBookAndAllowMovementAndStatus(LegalBookId, True, True)
    End Function

    ''' <summary>
    ''' Obtiene la entidad de MainAccount
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountById(Id As Integer) As XPCollection
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.GetAccountById(Id)
    End Function

#End Region

End Class