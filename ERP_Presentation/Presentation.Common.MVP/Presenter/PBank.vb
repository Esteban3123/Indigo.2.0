'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 08-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP
#End Region

''' <summary>
''' Presentador del frontal de corporaciones
''' </summary>
''' <remarks></remarks>
Public Class PBank

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IBank

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IBank)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

	''' <summary>
	''' Lista los centro de costo asociados a la cuenta contable con restricción
	''' </summary>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function ListMainAccountRestriction(mainAccountId As Integer)
		Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListMainAccountRestrictionBank(mainAccountId)
	End Function

	''' <summary>
	''' Lista todos los centros de costos
	''' </summary>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function LisAllCostCenter()
		Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
	End Function

	''' <summary>
	''' Inicializa el datasource de las ciudades
	''' </summary>
	Public Sub Initializes()
        Using Model As New MBusqueda()
            View.ThirdPartyDataSource = Model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    Public Async Sub GetSequence()
        Using model As New MCommonTreasury(View.MyTag)
            Me.View.Sequence = Await model.GetSequense()
        End Using
    End Sub

#End Region

End Class
