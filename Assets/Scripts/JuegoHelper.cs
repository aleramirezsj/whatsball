
using UnityEngine;

public static class JuegoHelper{

	public static Sprite obtenerFondo(DeportesEnum deporte){
		Sprite fondo;
		switch (deporte)
		{
			case DeportesEnum.Basquetbol:
				fondo=Resources.Load<Sprite>("Sprites/CanchaBasquet");
				break;
			case DeportesEnum.Futbol:
				fondo=Resources.Load<Sprite>("Sprites/CanchaFutbol");
				break;
			case DeportesEnum.Tenis:
				fondo=Resources.Load<Sprite>("Sprites/CanchaTenis");
				break;
			case DeportesEnum.Voley:
				fondo=Resources.Load<Sprite>("Sprites/CanchaVoley");
				break;
			default:
				fondo=Resources.Load<Sprite>("Sprites/CanchaBasket");
				break;
		}
		return fondo;
	}
	public static Sprite obtenerPelota(DeportesEnum deporte){
		Sprite pelota;
		switch (deporte)
		{
			case DeportesEnum.Basquetbol:
				pelota=Resources.Load<Sprite>("Sprites/basket");
				break;
			case DeportesEnum.Futbol:
				pelota=Resources.Load<Sprite>("Sprites/futbol");
				break;
			case DeportesEnum.Tenis:
				pelota=Resources.Load<Sprite>("Sprites/tenis");
				break;
			case DeportesEnum.Voley:
				pelota=Resources.Load<Sprite>("Sprites/voley");
				break;
			default:
				pelota=Resources.Load<Sprite>("Sprites/basket");
				break;
		}
		return pelota;
	}
}