using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShootDelays
{
    public const float PEESTOL = 0.28f;
}

public struct ReloadTime
{
    public const float PEESTOL = 3f;
}

public struct Damage
{
    public const float PEESTOL = 24.74f;
}

public class Weapon : MonoBehaviour
{
    AudioClip FIRE, RELOD, EMPTY;
    
    
    public enum weaponIDs
    {
        INVALID,
        PEESTOL
    }
     //clipSizes, damage, shooting delay

    public enum clipSize
    {
        PEESTOL = 17
    }

    
    

    string w_name;
    int w_clipSize, w_totalAmmo, w_ammoInClip, w_ID;

    bool firing = false;
    bool inUse;

    

    public string GunName
    {
        get { return w_name; }
        set { w_name = value; }
    }
    public int ClipSize
    {
        get { return w_clipSize; }
        set { w_clipSize = value; }
    }

    public int TotalAmmo
    {
        get { return w_totalAmmo; }
        set { w_totalAmmo = value; }
        
    }
    public int AmmoInClip
    {
        get { return w_ammoInClip; }
        set { w_ammoInClip = value; }
    }

    public int WeaponID
    {
        get { return w_ID; }
        set { w_ID = value; }
    }

    public bool InUse
    {
        get { return inUse; }
        set { inUse = value; }
    }

    public AudioClip Fire
    {
        get { return this.FIRE; }
        set { this.FIRE = value; }
    }

    public AudioClip RELOAD
    {
        get { return this.RELOD; }
        set { this.RELOD = value; }
    }

    public AudioClip Empty
    {
        get { return this.EMPTY; }
        set { this.EMPTY = value; }
    }
    //shoot function
    //
    //will shoot raycast from camera.screen.screenpointtoray
    //if it collides with gameObject with Player component, deals damage
    //
    //when shooting, the following events occur:

    //firing noise
    //one ammo gets consumed, not before checking if the clip isn't empty
    //if a Player is hit, deal damage appropriate for the weapon
    //***IMPLEMENT SHOOTING DELAY FOR EACH WEAPON***

    public void Shoot(int weaponID, AudioSource ads)
    {
        //get audiosource of player, yada yada yada
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Player playerHit = null;
        

        if (w_ammoInClip > 0 && !firing)
        {
            w_ammoInClip--;
            ads.PlayOneShot(FIRE);
            

            if (Physics.Raycast(ray, out hit))
            {
                playerHit = hit.transform.GetComponent<Player>();

                if (playerHit != null && playerHit.IsAlive)  //deal damage
                {
                    playerHit.TakeDamage(DamageFromID(this.w_ID));
                    
                }
                
            }
            StartCoroutine(ShootDelay(weaponID)); 

        }

       
        else if(w_ammoInClip < 1)
        {
            ads.PlayOneShot(EMPTY);
           
        }

        
    }

    IEnumerator ShootDelay(int id)
    {
        this.firing = true;

        float timeElapsed = 0f, delay = 0f;

        switch(id)
        {
            case (int)weaponIDs.INVALID:
                Debug.Log("invalid weapon shot");
                break;
            case (int)weaponIDs.PEESTOL: delay = ShootDelays.PEESTOL; break;
        }

        while (timeElapsed < delay)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        this.firing = false;
        
    }

    float DamageFromID(int id)
    {
        
        switch(id)
        {
            case (int)weaponIDs.INVALID:
                
                Debug.Log("Invalid damage from ID");
                return 0.1f;

            case (int)weaponIDs.PEESTOL: return Damage.PEESTOL;


            default:
               
                Debug.Log("ID parameter from DamageFromID function(Weapon.cs) not on weapon list.");
                return 0f;

        }
    }

    IEnumerator ReloadDelay(float delay)
    {
        this.firing = true;
        float timeElapsed = 0f;

        while(timeElapsed < delay)
        {
            timeElapsed += Time.deltaTime;
            yield return null;

        }
        this.w_totalAmmo -= w_ammoInClip > 1 ? (maxClipSize() - w_ammoInClip) : maxClipSize();
        this.w_ammoInClip = maxClipSize();

        this.firing = false;

    }

    public void Reload(AudioSource ads)
    {
        switch(w_ID)
        {
            case (int)weaponIDs.INVALID:
                Debug.Log("invalid weapon shot");
                break;
            case (int)weaponIDs.PEESTOL:

                if (w_totalAmmo > maxClipSize())
                {
                    ads.PlayOneShot(RELOAD);
                    StartCoroutine(ReloadDelay(ReloadTime.PEESTOL));
                    break;
                }
                else break;

        }
    }
    public void GiveAmmo(int amount)
    {
        w_totalAmmo += amount;
    }

    int maxClipSize() { return this.w_clipSize; }

    public void CreateGun(int ID, int totalAmmo, AudioClip fire, AudioClip reload, AudioClip empty)
    {
        switch(ID)
        {
            case (int)weaponIDs.PEESTOL:

                this.w_ID = ID;
                this.w_clipSize = (int)clipSize.PEESTOL;
                this.w_ammoInClip = maxClipSize();
                this.w_totalAmmo = totalAmmo;
                this.w_name = "Peestol";
                this.FIRE = fire;
                this.RELOAD = reload;
                this.EMPTY = empty;

                break;

            
        }
    }
    public bool AreValuesMatchingStock()
    {
        switch(this.w_ID)
        {
            case (int)weaponIDs.PEESTOL:
                if (this.w_clipSize == (int)clipSize.PEESTOL) return true;
                return false ;

            default: return false;
        }
    }
}
