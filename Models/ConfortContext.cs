using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackendConfortTravel.Models;

public partial class ConfortContext : DbContext
{
    public ConfortContext()
    {
    }

    public ConfortContext(DbContextOptions<ConfortContext> options)
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

    public virtual DbSet<TblFel> TblFels { get; set; }

    public virtual DbSet<TblHotel> TblHotels { get; set; }

    public virtual DbSet<TblImpuesto> TblImpuestos { get; set; }

    public virtual DbSet<TblItinerario> TblItinerarios { get; set; }

    public virtual DbSet<TblItinerarioHotel> TblItinerarioHotels { get; set; }

    public virtual DbSet<TblItinerarioPasajero> TblItinerarioPasajeros { get; set; }

    public virtual DbSet<TblOrdenDePago> TblOrdenDePagos { get; set; }

    public virtual DbSet<TblPagoPlanilla> TblPagoPlanillas { get; set; }

    public virtual DbSet<TblPersona> TblPersonas { get; set; }

    public virtual DbSet<TblPlanilla> TblPlanillas { get; set; }

    public virtual DbSet<TblRol> TblRols { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    public virtual DbSet<TblVehiculo> TblVehiculos { get; set; }

    public virtual DbSet<TblSalidaDestino> TblSalidaDestinos { get; set; }

    public virtual DbSet<TblSalida> TblSalidas { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=coronado; Database=ConfortTravel;  integrated security=True; Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

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
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaRetorno)
                .HasColumnType("date")
                .HasColumnName("fechaRetorno");
            entity.Property(e => e.FechaSalida)
                .HasColumnType("date")
                .HasColumnName("fechaSalida");
            entity.Property(e => e.IdDestino).HasColumnName("idDestino");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.PrecioCotizacion).HasColumnName("precioCotizacion");
            entity.Property(e => e.TotalAdultos).HasColumnName("totalAdultos");
            entity.Property(e => e.TotalDias).HasColumnName("totalDias");
            entity.Property(e => e.TotalNinos).HasColumnName("totalNinos");
            entity.Property(e => e.ValidoHasta)
                .HasColumnType("date")
                .HasColumnName("validoHasta");

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.TblCotizacions)
                .HasForeignKey(d => d.IdDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Cotizacion_tbl_Destino");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TblCotizacions)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Cotizacion_tbl_Usuario");

            entity.HasOne(d => d.IdSalidaDestinoNavigation).WithMany(p => p.TblCotizacions)
               .HasForeignKey(d => d.IdSalidaDestino)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_tbl_Cotizacion_tbl_SalidaDestino");
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

        modelBuilder.Entity<TblItinerario>(entity =>
        {
            entity.HasKey(e => e.IdItinerario);

            entity.ToTable("tbl_Itinerario");

            entity.Property(e => e.IdItinerario)
                .ValueGeneratedNever()
                .HasColumnName("idItinerario");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaRetorno)
                .HasColumnType("date")
                .HasColumnName("fechaRetorno");
            entity.Property(e => e.FechaSalida)
                .HasColumnType("date")
                .HasColumnName("fechaSalida");
            entity.Property(e => e.HoraRetorno).HasColumnName("horaRetorno");
            entity.Property(e => e.HoraSalida).HasColumnName("horaSalida");
            entity.Property(e => e.IdDestino).HasColumnName("idDestino");
            entity.Property(e => e.IdVehiculo).HasColumnName("idVehiculo");
            entity.Property(e => e.TotalDias)
                .HasMaxLength(50)
                .HasColumnName("totalDias");

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.TblItinerarios)
                .HasForeignKey(d => d.IdDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Itinerario_tbl_Destino");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.TblItinerarios)
                .HasForeignKey(d => d.IdVehiculo)
                .HasConstraintName("FK_tbl_Itinerario_tbl_Transporte");
        });

        modelBuilder.Entity<TblItinerarioHotel>(entity =>
        {
            entity.HasKey(e => e.IdItinerarioHotel).HasName("PK_tbl_Alojamiento");

            entity.ToTable("tbl_ItinerarioHotel");

            entity.Property(e => e.IdItinerarioHotel)
                .ValueGeneratedNever()
                .HasColumnName("idItinerarioHotel");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaFin)
                .HasColumnType("date")
                .HasColumnName("fechaFin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("date")
                .HasColumnName("fechaInicio");
            entity.Property(e => e.IdHotel).HasColumnName("idHotel");
            entity.Property(e => e.IdItinerario).HasColumnName("idItinerario");
            entity.Property(e => e.TotalDias)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("totalDias");

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.TblItinerarioHotels)
                .HasForeignKey(d => d.IdHotel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Alojamiento_tbl_Hotel");

            entity.HasOne(d => d.IdItinerarioNavigation).WithMany(p => p.TblItinerarioHotels)
                .HasForeignKey(d => d.IdItinerario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Alojamiento_tbl_Itinerario");
        });

        modelBuilder.Entity<TblItinerarioPasajero>(entity =>
        {
            entity.HasKey(e => e.IdPasajeroItinerario);

            entity.ToTable("tbl_ItinerarioPasajero");

            entity.Property(e => e.IdPasajeroItinerario).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdItinerario).HasColumnName("idItinerario");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");

            entity.HasOne(d => d.IdItinerarioNavigation).WithMany(p => p.TblItinerarioPasajeros)
                .HasForeignKey(d => d.IdItinerario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_ItinerarioPasajero_tbl_Itinerario");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TblItinerarioPasajeros)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_ItinerarioPasajero_tbl_Persona");
        });

        modelBuilder.Entity<TblOrdenDePago>(entity =>
        {
            entity.HasKey(e => e.IdOrdenDePago);

            entity.ToTable("tbl_OrdenDePago");

            entity.Property(e => e.IdOrdenDePago)
                .ValueGeneratedNever()
                .HasColumnName("idOrdenDePago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
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

        modelBuilder.Entity<TblPersona>(entity =>
        {
            entity.HasKey(e => e.IdPersona);

            entity.ToTable("tbl_Persona");

            entity.Property(e => e.IdPersona)
                .ValueGeneratedNever()
                .HasColumnName("idPersona");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .HasColumnName("direccion");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
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
            entity.Property(e => e.Dpi).HasColumnName("dpi");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .HasColumnName("estadoCivil");
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("date")
                .HasColumnName("fechaContratacion");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fechaNacimiento");
            entity.Property(e => e.HorasTrabajadas).HasColumnName("horasTrabajadas");
            entity.Property(e => e.IdCargo).HasColumnName("idCargo");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.SalarioBase).HasColumnName("salarioBase");
            entity.Property(e => e.TarifaPorHora).HasColumnName("tarifaPorHora");
            entity.Property(e => e.TiempoDeContrato)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tiempoDeContrato");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.TblPlanillas)
                .HasForeignKey(d => d.IdCargo)
                .HasConstraintName("FK_tbl_Planilla_tbl_CargoLaboral");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TblPlanillas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Planilla_tbl_Usuario");
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

        modelBuilder.Entity<TblSalidaDestino>(entity =>
        {
            entity.HasKey(e => e.IdSalidaDestino).HasName("PK_tbl_SalidaDestino");

            entity.ToTable("tbl_SalidaDestino");

            entity.Property(e => e.IdSalidaDestino)
                .ValueGeneratedNever()
                .HasColumnName("idSalidaDestino");
            entity.Property(e => e.Estado).HasColumnName("estado");
           
            entity.Property(e => e.IdSalida).HasColumnName("idSalida");

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.TblSalidaDestinos)
                .HasForeignKey(d => d.IdDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_SalidaDestino_tbl_Destino");

            entity.HasOne(d => d.IdSalidaNavigation).WithMany(p => p.TblSalidaDestinos)
               .HasForeignKey(d => d.IdSalida)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_tbl_SalidaDestino_tbl_Salida");


        });

        modelBuilder.Entity<TblSalida>(entity =>
        {
            entity.HasKey(e => e.IdSalida).HasName("PK_tbl_Salida");

            entity.ToTable("tbl_Salida");

            entity.Property(e => e.IdSalida)
                .ValueGeneratedNever()
                .HasColumnName("idSalida");

            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.Estado).HasColumnName("estado");


        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
