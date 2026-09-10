Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

Public Class PReportExogenousInformation

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IReportExogenousInformation

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IReportExogenousInformation)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para obtener el nivel máximo de estructura organizacional
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionExogenousFormat()
        View.FormatXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListCollectionExogenousFormat($"Status={True}")
    End Sub

    ''' <summary>
    ''' Obtiene el Tercero por el Id del Cliente
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetExogenousFormatById(Id As Integer) As AccountingRepository.ExogenousFormatXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetCollection(Of AccountingRepository.ExogenousFormatXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
