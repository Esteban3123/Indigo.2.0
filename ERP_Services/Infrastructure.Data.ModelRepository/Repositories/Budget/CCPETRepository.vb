Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class CCPETRepository
    Inherits GenericRepository(Of CCPET)
    Implements ICCPETRepository

    'Coexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un CCPET por código
    ''' </summary>
    ''' <param name="Code">Código del CCPET </param>
    ''' <returns></returns>
    Public Function GetCCPETByCodeAndItemType(Code As String) As CCPET Implements ICCPETRepository.GetCCPETByCode
        Dim result As CCPET = (From e In _context.CCPET
                               Where e.Code = Code
                               Select e).FirstOrDefault

        If result IsNot Nothing Then
            result.ItemtypeName = If(result.ItemType = 1, "Ingresos", "Gastos")
            result.AccountTypeName = If(result.AccountType, "Captura (C)", "Agregacion (A)")

            If result.CCPETOwnerId IsNot Nothing Then
                Dim parent = (From p In _context.CCPET.AsNoTracking Where p.Id = result.CCPETOwnerId Select p).FirstOrDefault
                result.ParentDescription = parent.Code + " - " + parent.Name
            End If

            result.OriginalValue = (From e In _context.CCPET.AsNoTracking Where e.Id = result.Id Select e).FirstOrDefault

            Return result
        Else
            Return New CCPET
        End If
    End Function

    ''' <summary>
    ''' Consulta un rubro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCCPETById(Id As Integer) As CCPET Implements ICCPETRepository.GetCCPETById
        Return (From c In _context.CCPET.AsNoTracking() Where c.Id = Id Select c).FirstOrDefault
    End Function

#End Region

End Class
