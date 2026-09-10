'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Oscar Astudillo Reyes
' Created          : 2024-10-15
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
#End Region

Public Class PPMedicationType

#Region "Fields"

    ''' <summary>
    ''' Instancia de la interfaz
    ''' </summary>
    Dim _view As IMedicationType

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim _indigoSessionValues As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As IMedicationType)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigoSessionValues = SessionValues.Instance
            Me._view = view
        End If
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga la definición del layout de forma asíncrona.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await _view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    '''  Obtiene la secuencia de registros de forma asíncrona.
    ''' </summary>
    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequense(_view.MyTag)
            _view.Sequence = Await model.GetSequense()
        End Using
    End Sub


    ''' <summary>
    ''' Obtiene registro por Codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Async Function GetMedicationTypeByCode(ByVal Code As String) As Task(Of ActionResult(Of MedicationType))
        Using model As New MMedicationType(_view.MyTag.ToString())
            Dim result As ActionResult(Of MedicationType) = Await model.GetByCode(Code)
            Return result
        End Using
    End Function

    ''' <summary>
    ''' Guarda registro
    ''' </summary>
    ''' <param name="medicationType"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Async Function SaveMedicationTypeAsync(ByVal medicationType As MedicationType, ByVal idSequence As Integer) As Task(Of ActionResult(Of MedicationType))
        If medicationType Is Nothing Then
            Throw New ArgumentNullException(NameOf(medicationType), "Objeto vacio")
        End If
        If _view.MyTag Is Nothing Then
            Throw New ArgumentException("MyTag no puede ser nulo.", NameOf(_view.MyTag))
        End If
        Using model As New MMedicationType(_view.MyTag.ToString())
            Dim result As ActionResult(Of MedicationType) = Await model.Save(medicationType, idSequence)
            Return result
        End Using
    End Function

    ''' <summary>
    ''' Elimina registro
    ''' </summary>
    ''' <param name="medicationType"></param>
    ''' <returns></returns>
    Public Function DeleteMedicationTypeAsync(ByVal medicationType As MedicationType) As Task(Of ActionResult)
        Using model As New MMedicationType(_view.MyTag.ToString())
            Dim result As Task(Of ActionResult) = model.Delete(medicationType)
            Return result
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado del registro
    ''' </summary>
    ''' <param name="MedicationType"></param>
    ''' <param name="state">.</param>
    Public Async Function ChangeStateMedicationType(ByVal medicationType As MedicationType, state As Boolean) As Task(Of ActionResult(Of MedicationType))
        Using model As New MMedicationType(_view.MyTag.ToString())
            Dim result As ActionResult(Of MedicationType) = Await model.ChangeState(medicationType, state)
            Return result
        End Using
    End Function


#End Region

End Class
