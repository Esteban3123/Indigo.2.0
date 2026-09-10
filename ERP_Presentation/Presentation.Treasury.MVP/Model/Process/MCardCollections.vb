'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Andrés Steven Rojas 
' Created          : 04/11/2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo
Imports Domain.Treasury.Model
#End Region
Public Class MCardCollections
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene un registro de recaudo de tarjetas por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetCardCollectionsById(id As Integer) As Task(Of CardCollections)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCardCollectionsByIdAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene un registro de recaudo de tarjetas por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Async Function GetCardCollections(ByVal code As String, ByVal audit As AuditMessage) As Task(Of ActionResult(Of CardCollections))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCardCollectionsAsync(code, audit)
    End Function

    ''' <summary>
    ''' Guarda un registro de recaudo de tarjetas
    ''' </summary>
    ''' <param name="cardCollections">The card collections.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Public Async Function SaveCardCollections(ByVal cardCollections As CardCollections, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As Task(Of ActionResult(Of CardCollections))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveCardCollectionsAsync(cardCollections, idSequence, audit)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
