#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.PayrollRepository

#End Region

Public Class PLoadMassive

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ILoadMassive

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ILoadMassive)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub
    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function


#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numérica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequensePayments(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

#End Region

End Class
