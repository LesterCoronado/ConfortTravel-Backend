using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackendConfortTravel.Models;

public partial class ConfortTravelContext : DbContext
{
    public ConfortTravelContext()
    {
    }

    public ConfortTravelContext(DbContextOptions<ConfortTravelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAsignarRol> TblAsignarRols { get; set; }

    public virtual DbSet<TblBono> TblBonos { get; set; }

    public virtual DbSet<TblBonoPlanilla> TblBonoPlanillas { get; set; }

    public virtual DbSet<TblCargoLaboral> TblCargoLaborals { get; set; }

    public virtual DbSet<TblCotizacion> TblCotizacions { get; set; }

    public virtual DbSet<TblDeptoTrabajo> TblDeptoTrabajos { get; set; }

    public virtual DbSet<TblDescuento> TblDescuentos { get; set; }

    public virtual DbSet<TblDescuentoPlanilla> TblDescuentoPlanillas { get; set; }

    public virtual DbSet<TblDestino> TblDestinos { get; set; }

    public virtual DbSet<TblDestinoPaquete> TblDestinoPaquetes { get; set; }

    public virtual DbSet<TblEmpleado> TblEmpleados { get; set; }

    public virtual DbSet<TblFel> TblFels { get; set; }

    public virtual DbSet<TblHotel> TblHotels { get; set; }

    public virtual DbSet<TblImpuesto> TblImpuestos { get; set; }

    public virtual DbSet<TblOrdenDePago> TblOrdenDePagos { get; set; }

    public virtual DbSet<TblPagoPlanilla> TblPagoPlanillas { get; set; }

    public virtual DbSet<TblPaqueteIncluye> TblPaqueteIncluyes { get; set; }

    public virtual DbSet<TblPaqueteItinerario> TblPaqueteItinerarios { get; set; }

    public virtual DbSet<TblPaqueteNoIncluye> TblPaqueteNoIncluyes { get; set; }

    public virtual DbSet<TblPaqueteViaje> TblPaqueteViajes { get; set; }

    public virtual DbSet<TblPasajeroReserva> TblPasajeroReservas { get; set; }

    public virtual DbSet<TblPersona> TblPersonas { get; set; }

    public virtual DbSet<TblPlanilla> TblPlanillas { get; set; }

    public virtual DbSet<TblReserva> TblReservas { get; set; }

    public virtual DbSet<TblReservaAlojamiento> TblReservaAlojamientos { get; set; }

    public virtual DbSet<TblRol> TblRols { get; set; }

    public virtual DbSet<TblSalidum> TblSalida { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    public virtual DbSet<TblVehiculo> TblVehiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=coronado; Database=ConfortTravel;  integrated security=True; Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAsignarRol>(entity =>
        {
            entity.HasKey(e => e.IdAsignarRol);

            entity.ToTable("tbl_AsignarRol");

            entity.Property(e => e.IdAsignarRol)
                .ValueGeneratedNever()
                .HasColumnName("idAsignarRol");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TblAsignarRols)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_AsignarRol_tbl_Rol");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TblAsignarRols)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_AsignarRol_tbl_Usuario");
        });

        modelBuilder.Entity<TblBono>(entity =>
        {
            entity.HasKey(e => e.IdBono);

            entity.ToTable("tbl_Bono");

            entity.Property(e => e.IdBono)
                .ValueGeneratedNever()
                .HasColumnName("idBono");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FrecuenciaPago)
                .HasMaxLength(50)
                .HasColumnName("frecuenciaPago");
            entity.Property(e => e.Moneda)
                .HasMaxLength(50)
                .HasColumnName("moneda");
            entity.Property(e => e.Monto).HasColumnName("monto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TblBonoPlanilla>(entity =>
        {
            entity.HasKey(e => e.IdBonoPlanilla);

            entity.ToTable("tbl_BonoPlanilla");

            entity.Property(e => e.IdBonoPlanilla)
                .ValueGeneratedNever()
                .HasColumnName("idBonoPlanilla");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaAsignacion)
                .HasColumnType("date")
                .HasColumnName("fechaAsignacion");
            entity.Property(e => e.IdPlanilla).HasColumnName("idPlanilla");

            entity.HasOne(d => d.IdBonoPlanillaNavigation).WithOne(p => p.TblBonoPlanilla)
                .HasForeignKey<TblBonoPlanilla>(d => d.IdBonoPlanilla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_BonoPlanilla_tbl_Bono");

            entity.HasOne(d => d.IdPlanillaNavigation).WithMany(p => p.TblBonoPlanillas)
                .HasForeignKey(d => d.IdPlanilla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_BonoPlanilla_tbl_Planilla");
        });

        modelBuilder.Entity<TblCargoLaboral>(entity =>
        {
            entity.HasKey(e => e.IdCargoLaboral);

            entity.ToTable("tbl_CargoLaboral");

            entity.Property(e => e.IdCargoLaboral)
                .ValueGeneratedNever()
                .HasColumnName("idCargoLaboral");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdDeptoTrabajo).HasColumnName("idDeptoTrabajo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdDeptoTrabajoNavigation).WithMany(p => p.TblCargoLaborals)
                .HasForeignKey(d => d.IdDeptoTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_CargoLaboral_tbl_DeptoTrabajo");
        });

        modelBuilder.Entity<TblCotizacion>(entity =>
        {
            entity.HasKey(e => e.IdCotizacion).HasName("PK_tbl_cotizacion");

            entity.ToTable("tbl_Cotizacion");

            entity.Property(e => e.IdCotizacion)
                .ValueGeneratedNever()
                .HasColumnName("idCotizacion");
            entity.Property(e => e.Comentario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("comentario");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaSalida)
                .HasColumnType("date")
                .HasColumnName("fechaSalida");
            entity.Property(e => e.IdPaqueteViaje).HasColumnName("idPaqueteViaje");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.PrecioCotizacion).HasColumnName("precioCotizacion");
            entity.Property(e => e.TotalAdultos).HasColumnName("totalAdultos");
            entity.Property(e => e.TotalNinos).HasColumnName("totalNinos");
            entity.Property(e => e.ValidoHasta)
                .HasColumnType("date")
                .HasColumnName("validoHasta");

            entity.HasOne(d => d.IdPaqueteViajeNavigation).WithMany(p => p.TblCotizacions)
                .HasForeignKey(d => d.IdPaqueteViaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Cotizacion_tbl_PaqueteViaje");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TblCotizacions)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Cotizacion_tbl_Usuario");
        });

        modelBuilder.Entity<TblDeptoTrabajo>(entity =>
        {
            entity.HasKey(e => e.IdDeptoTrabajo);

            entity.ToTable("tbl_DeptoTrabajo");

            entity.Property(e => e.IdDeptoTrabajo)
                .ValueGeneratedNever()
                .HasColumnName("idDeptoTrabajo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TblDescuento>(entity =>
        {
            entity.HasKey(e => e.IdDescuento);

            entity.ToTable("tbl_Descuento");

            entity.Property(e => e.IdDescuento)
                .ValueGeneratedNever()
                .HasColumnName("idDescuento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FrecuenciaDescuento)
                .HasMaxLength(50)
                .HasColumnName("frecuenciaDescuento");
            entity.Property(e => e.Moneda)
                .HasMaxLength(50)
                .HasColumnName("moneda");
            entity.Property(e => e.Monto).HasColumnName("monto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TblDescuentoPlanilla>(entity =>
        {
            entity.HasKey(e => e.IdDescuento);

            entity.ToTable("tbl_DescuentoPlanilla");

            entity.Property(e => e.IdDescuento)
                .ValueGeneratedNever()
                .HasColumnName("idDescuento");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaAsignacion)
                .HasColumnType("date")
                .HasColumnName("fechaAsignacion");
            entity.Property(e => e.IdPlanilla).HasColumnName("idPlanilla");

            entity.HasOne(d => d.IdDescuentoNavigation).WithOne(p => p.TblDescuentoPlanilla)
                .HasForeignKey<TblDescuentoPlanilla>(d => d.IdDescuento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_DescuentoPlanilla_tbl_Descuento");

            entity.HasOne(d => d.IdPlanillaNavigation).WithMany(p => p.TblDescuentoPlanillas)
                .HasForeignKey(d => d.IdPlanilla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_DescuentoPlanilla_tbl_Planilla");
        });

        modelBuilder.Entity<TblDestino>(entity =>
        {
            entity.HasKey(e => e.IdDestino);

            entity.ToTable("tbl_Destino");

            entity.Property(e => e.IdDestino).ValueGeneratedNever();
            entity.Property(e => e.Depto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("depto");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Imagen)
                .IsUnicode(false)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pais");
        });

        modelBuilder.Entity<TblDestinoPaquete>(entity =>
        {
            entity.HasKey(e => e.IdDestinoPaquete);

            entity.ToTable("tbl_DestinoPaquete");

            entity.Property(e => e.IdDestinoPaquete)
                .ValueGeneratedNever()
                .HasColumnName("idDestinoPaquete");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdDestino).HasColumnName("idDestino");
            entity.Property(e => e.IdPaquete).HasColumnName("idPaquete");

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.TblDestinoPaquetes)
                .HasForeignKey(d => d.IdDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_DestinoPaquete_tbl_Destino");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.TblDestinoPaquetes)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_DestinoPaquete_tbl_PaqueteViaje");
        });

        modelBuilder.Entity<TblEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado);

            entity.ToTable("tbl_Empleado");

            entity.Property(e => e.IdEmpleado)
                .ValueGeneratedNever()
                .HasColumnName("idEmpleado");
            entity.Property(e => e.Altura).HasColumnName("altura");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estadoCivil");
            entity.Property(e => e.FechaNac)
                .HasColumnType("date")
                .HasColumnName("fechaNac");
            entity.Property(e => e.FormacionAcademica)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("formacionAcademica");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.Peso).HasColumnName("peso");
            entity.Property(e => e.TipoSangre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipoSangre");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TblEmpleados)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Empleado_tbl_Persona");
        });

        modelBuilder.Entity<TblFel>(entity =>
        {
            entity.HasKey(e => e.IdFel);

            entity.ToTable("tbl_FEL");

            entity.Property(e => e.IdFel)
                .ValueGeneratedNever()
                .HasColumnName("idFel");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.IdImpuesto).HasColumnName("idImpuesto");
            entity.Property(e => e.IdOrdenDePago).HasColumnName("idOrdenDePago");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Moneda)
                .HasMaxLength(50)
                .HasColumnName("moneda");
            entity.Property(e => e.Nit).HasColumnName("nit");
            entity.Property(e => e.NumeroFactura).HasColumnName("numeroFactura");
            entity.Property(e => e.Serie)
                .HasMaxLength(50)
                .HasColumnName("serie");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");

            entity.HasOne(d => d.IdImpuestoNavigation).WithMany(p => p.TblFels)
                .HasForeignKey(d => d.IdImpuesto)
                .HasConstraintName("FK_tbl_FEL_tbl_Impuesto");

            entity.HasOne(d => d.IdOrdenDePagoNavigation).WithMany(p => p.TblFels)
                .HasForeignKey(d => d.IdOrdenDePago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_FEL_tbl_OrdenDePago");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TblFels)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_FEL_tbl_Usuario");
        });

        modelBuilder.Entity<TblHotel>(entity =>
        {
            entity.HasKey(e => e.IdHotel);

            entity.ToTable("tbl_Hotel");

            entity.Property(e => e.IdHotel)
                .ValueGeneratedNever()
                .HasColumnName("idHotel");
            entity.Property(e => e.Depto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("depto");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pais");
            entity.Property(e => e.Telefono).HasColumnName("telefono");
        });

        modelBuilder.Entity<TblImpuesto>(entity =>
        {
            entity.HasKey(e => e.IdImpuesto);

            entity.ToTable("tbl_Impuesto");

            entity.Property(e => e.IdImpuesto)
                .ValueGeneratedNever()
                .HasColumnName("idImpuesto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");
        });

        modelBuilder.Entity<TblOrdenDePago>(entity =>
        {
            entity.HasKey(e => e.IdOrdenDePago);

            entity.ToTable("tbl_OrdenDePago");

            entity.Property(e => e.IdOrdenDePago)
                .ValueGeneratedNever()
                .HasColumnName("idOrdenDePago");
            entity.Property(e => e.Checkout)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("checkout");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaGenerado)
                .HasColumnType("date")
                .HasColumnName("fechaGenerado");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("date")
                .HasColumnName("fechaVencimiento");
            entity.Property(e => e.IdCotizacion).HasColumnName("idCotizacion");

            entity.HasOne(d => d.IdCotizacionNavigation).WithMany(p => p.TblOrdenDePagos)
                .HasForeignKey(d => d.IdCotizacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_OrdenDePago_tbl_Cotizacion");
        });

        modelBuilder.Entity<TblPagoPlanilla>(entity =>
        {
            entity.HasKey(e => e.IdPagoPlanilla);

            entity.ToTable("tbl_PagoPlanilla");

            entity.Property(e => e.IdPagoPlanilla)
                .ValueGeneratedNever()
                .HasColumnName("idPagoPlanilla");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.IdPlanilla).HasColumnName("idPlanilla");
            entity.Property(e => e.Moneda)
                .HasMaxLength(50)
                .HasColumnName("moneda");
            entity.Property(e => e.Monto).HasColumnName("monto");

            entity.HasOne(d => d.IdPlanillaNavigation).WithMany(p => p.TblPagoPlanillas)
                .HasForeignKey(d => d.IdPlanilla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_PagoPlanilla_tbl_Planilla");
        });

        modelBuilder.Entity<TblPaqueteIncluye>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteIncluye);

            entity.ToTable("tbl_PaqueteIncluye");

            entity.Property(e => e.IdPaqueteIncluye)
                .ValueGeneratedNever()
                .HasColumnName("idPaqueteIncluye");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdPaqueteViaje).HasColumnName("idPaqueteViaje");
            entity.Property(e => e.Incluye)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("incluye");

            entity.HasOne(d => d.IdPaqueteViajeNavigation).WithMany(p => p.TblPaqueteIncluyes)
                .HasForeignKey(d => d.IdPaqueteViaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_PaqueteIncluye_tbl_PaqueteViaje");
        });

        modelBuilder.Entity<TblPaqueteItinerario>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteItinerario);

            entity.ToTable("tbl_PaqueteItinerario");

            entity.Property(e => e.IdPaqueteItinerario)
                .ValueGeneratedNever()
                .HasColumnName("idPaqueteItinerario");
            entity.Property(e => e.Actividad)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("actividad");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Horario)
                .HasPrecision(0)
                .HasColumnName("horario");
            entity.Property(e => e.IdPaqueteViaje).HasColumnName("idPaqueteViaje");

            entity.HasOne(d => d.IdPaqueteViajeNavigation).WithMany(p => p.TblPaqueteItinerarios)
                .HasForeignKey(d => d.IdPaqueteViaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_PaqueteItinerario_tbl_PaqueteViaje");
        });

        modelBuilder.Entity<TblPaqueteNoIncluye>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteNoIncluye);

            entity.ToTable("tbl_PaqueteNoIncluye");

            entity.Property(e => e.IdPaqueteNoIncluye)
                .ValueGeneratedNever()
                .HasColumnName("idPaqueteNoIncluye");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.IdPaqueteViaje).HasColumnName("idPaqueteViaje");
            entity.Property(e => e.NoIncluye)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("noIncluye");

            entity.HasOne(d => d.IdPaqueteViajeNavigation).WithMany(p => p.TblPaqueteNoIncluyes)
                .HasForeignKey(d => d.IdPaqueteViaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_PaqueteNoIncluye_tbl_PaqueteViaje");
        });

        modelBuilder.Entity<TblPaqueteViaje>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteViaje);

            entity.ToTable("tbl_PaqueteViaje");

            entity.Property(e => e.IdPaqueteViaje)
                .ValueGeneratedNever()
                .HasColumnName("idPaqueteViaje");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdSalida).HasColumnName("idSalida");
            entity.Property(e => e.MaxPax).HasColumnName("maxPax");
            entity.Property(e => e.MinPax).HasColumnName("minPax");
            entity.Property(e => e.ModalidadPaquete)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modalidadPaquete");
            entity.Property(e => e.PoliticaCancelacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("politicaCancelacion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo");
            entity.Property(e => e.TotalDias).HasColumnName("totalDias");
            entity.Property(e => e.TotalNoches).HasColumnName("totalNoches");

            entity.HasOne(d => d.IdSalidaNavigation).WithMany(p => p.TblPaqueteViajes)
                .HasForeignKey(d => d.IdSalida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_PaqueteViaje_tbl_Salida");
        });

        modelBuilder.Entity<TblPasajeroReserva>(entity =>
        {
            entity.HasKey(e => e.IdPasajeroReserva).HasName("PK_tbl_ItinerarioPasajero");

            entity.ToTable("tbl_PasajeroReserva");

            entity.Property(e => e.IdPasajeroReserva).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.IdReserva).HasColumnName("idReserva");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TblPasajeroReservas)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_ItinerarioPasajero_tbl_Persona");
        });

        modelBuilder.Entity<TblPersona>(entity =>
        {
            entity.HasKey(e => e.IdPersona);

            entity.ToTable("tbl_Persona");

            entity.Property(e => e.IdPersona)
                .ValueGeneratedNever()
                .HasColumnName("idPersona");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.DpiCedula).HasColumnName("dpi/cedula");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Sexo).HasColumnName("sexo");
            entity.Property(e => e.Telefono).HasColumnName("telefono");
        });

        modelBuilder.Entity<TblPlanilla>(entity =>
        {
            entity.HasKey(e => e.IdPlanilla);

            entity.ToTable("tbl_Planilla");

            entity.Property(e => e.IdPlanilla)
                .ValueGeneratedNever()
                .HasColumnName("idPlanilla");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("date")
                .HasColumnName("fechaContratacion");
            entity.Property(e => e.FechaDeBaja)
                .HasColumnType("date")
                .HasColumnName("fechaDeBaja");
            entity.Property(e => e.IdCargo).HasColumnName("idCargo");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.NoCuenta).HasColumnName("noCuenta");
            entity.Property(e => e.SalarioBase).HasColumnName("salarioBase");
            entity.Property(e => e.TiempoContrato).HasColumnName("tiempoContrato");
            entity.Property(e => e.TipoContrato)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoContrato");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.TblPlanillas)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Planilla_tbl_CargoLaboral");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.TblPlanillas)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Planilla_tbl_Empleado");
        });

        modelBuilder.Entity<TblReserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva);

            entity.ToTable("tbl_Reserva");

            entity.Property(e => e.IdReserva)
                .ValueGeneratedNever()
                .HasColumnName("idReserva");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaRetorno)
                .HasColumnType("date")
                .HasColumnName("fechaRetorno");
            entity.Property(e => e.FechaSalida)
                .HasColumnType("date")
                .HasColumnName("fechaSalida");
            entity.Property(e => e.HoraRetorno).HasColumnName("horaRetorno");
            entity.Property(e => e.HoraSalida)
                .HasPrecision(0)
                .HasColumnName("horaSalida");
            entity.Property(e => e.IdPaqueteViaje).HasColumnName("idPaqueteViaje");
            entity.Property(e => e.IdVehiculo).HasColumnName("idVehiculo");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.TotalDias).HasColumnName("totalDias");

            entity.HasOne(d => d.IdPaqueteViajeNavigation).WithMany(p => p.TblReservas)
                .HasForeignKey(d => d.IdPaqueteViaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Reserva_tbl_PaqueteViaje");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.TblReservas)
                .HasForeignKey(d => d.IdVehiculo)
                .HasConstraintName("FK_tbl_Reserva_tbl_Vehiculo");
        });

        modelBuilder.Entity<TblReservaAlojamiento>(entity =>
        {
            entity.HasKey(e => e.IdReservaAlojamiento).HasName("PK_tbl_Alojamiento");

            entity.ToTable("tbl_ReservaAlojamiento");

            entity.Property(e => e.IdReservaAlojamiento)
                .ValueGeneratedNever()
                .HasColumnName("idReservaAlojamiento");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaFin)
                .HasColumnType("date")
                .HasColumnName("fechaFin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("date")
                .HasColumnName("fechaInicio");
            entity.Property(e => e.IdHotel).HasColumnName("idHotel");
            entity.Property(e => e.IdReserva).HasColumnName("idReserva");
            entity.Property(e => e.TotalDias)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("totalDias");

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.TblReservaAlojamientos)
                .HasForeignKey(d => d.IdHotel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Alojamiento_tbl_Hotel");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.TblReservaAlojamientos)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_ReservaAlojamiento_tbl_Reserva");
        });

        modelBuilder.Entity<TblRol>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("tbl_Rol");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .HasColumnName("nombreRol");
        });

        modelBuilder.Entity<TblSalidum>(entity =>
        {
            entity.HasKey(e => e.IdSalida);

            entity.ToTable("tbl_Salida");

            entity.Property(e => e.IdSalida)
                .ValueGeneratedNever()
                .HasColumnName("idSalida");
            entity.Property(e => e.Direccion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<TblUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("tbl_Usuario");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("idUsuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(100)
                .HasColumnName("contraseña");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("date")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TblUsuarios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Usuario_tbl_Persona");
        });

        modelBuilder.Entity<TblVehiculo>(entity =>
        {
            entity.HasKey(e => e.IdVehiculo).HasName("PK_tbl_Transporte");

            entity.ToTable("tbl_Vehiculo");

            entity.Property(e => e.IdVehiculo)
                .ValueGeneratedNever()
                .HasColumnName("idVehiculo");
            entity.Property(e => e.CapacidadPasajeros).HasColumnName("capacidadPasajeros");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .HasColumnName("modelo");
            entity.Property(e => e.Placa)
                .HasMaxLength(50)
                .HasColumnName("placa");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
