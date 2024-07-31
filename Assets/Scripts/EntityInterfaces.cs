
public interface IEntity
{
    float HP { get;  set; }
}

public interface IShoot
{ 
    float shootSpeed { set; }

    float shootDamage { get; set; }
}

public interface IMove
{
    float speed { get; }

}

