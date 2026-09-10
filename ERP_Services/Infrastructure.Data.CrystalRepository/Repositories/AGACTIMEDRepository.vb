Imports System.Linq.Expressions
Imports Domain.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Infrastructure.Data.Base

Public Class AGACTIMEDRepository
    Inherits GenericRepository(Of AGACTIMED)
    Implements IAGACTIMEDRepository

#Region "Builder"

    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

#End Region

#Region "Methods"

    Public Function GetListAGACTIMEDPOCO(listAGACTIMEDCode As List(Of String)) As List(Of AGACTIMED) Implements IAGACTIMEDRepository.GetListAGACTIMEDPOCO
        If listAGACTIMEDCode Is Nothing OrElse listAGACTIMEDCode.Count = 0 Then
            Return New List(Of AGACTIMED)
        End If
        Return (From e In _crystalContext.AGACTIMED.AsNoTracking() Where listAGACTIMEDCode.Contains(e.CODACTMED) Select e).ToList()
    End Function

#End Region

End Class
