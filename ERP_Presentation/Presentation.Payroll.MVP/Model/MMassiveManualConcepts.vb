Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Presentation.CloudAgent

Public Class MMassiveManualConcepts
    Inherits ModelBase
    Implements IDisposable

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

    Public Function ValidateManualConceptsMassive(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveManualConcepts_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ValidateManualConceptsMassive(pData, Indigo)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub

    Public Function GetManualConceptsMassive(pData As List(Of ImportFileRow)) As List(Of SP_GetMassiveManualConcepts_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetManualConceptsMassive(pData, Indigo)
    End Function

    Public Sub SaveManualConceptsMassive(pMassiveManualConcepts As List(Of ImportFileRow))
        IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveManualConceptsMassive(pMassiveManualConcepts, Indigo)
    End Sub

#End Region

End Class
