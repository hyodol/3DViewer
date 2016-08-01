var target : Transform;


var xSpeed = 250.0;
var ySpeed = 120.0;
var movSpeed = 250.0;

var yMinLimit = -20;
var yMaxLimit = 80;

var zoomSpeed = 250;
var zoomMin = 1;
var zoomMax = 80;



private var x = 0.0;
private var y = 0.0;

private var distance :float;
private var movX : float;
private var movY : float;
private var distanceBk : float;
private var cameraBk : Vector3;

function Start () {
    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;

    cameraBk = GetComponent.<Camera>().transform.position;
	distance = -cameraBk.z;
    distanceBk = distance;
    // Make the rigid body not change rotation
   	if (GetComponent.<Rigidbody>())
		GetComponent.<Rigidbody>().freezeRotation = true;
}




function LateUpdate () {
    // 繝ｪ繧ｻ繝?ヨ
    if (Input.GetKeyDown("e")) {
    	transform.rotation =Quaternion.Euler(0, 0, 0);
    	transform.position = Vector3(0,0,distanceBk);
    	target.rotation =Quaternion.Euler(0, 0, 0);
    	target.position = Vector3(0,0,0);
		distance = distanceBk;
    	x=0;
    	y=0;
    	print("ool");
    }
    
    // 繧ｫ繝｡繝ｩ繧ｺ繝ｼ繝?
    if (Input.GetMouseButton(2) || Input.GetAxis("Mouse ScrollWheel") ) {
	    distance += (Input.GetAxis("Mouse Y") + Input.GetAxis("Mouse ScrollWheel") * 10 ) * zoomSpeed * 0.02;
	    if (distance < zoomMin) distance = zoomMin;
	    if (distance > zoomMax) distance = zoomMax;
    }

	// 繝槭え繧ｹ縺ｮ遘ｻ蜍暮㍼繧偵ち繝ｼ繧ｲ繝?ヨ縺ｮ遘ｻ蜍暮㍼縺ｨ縺吶ｋ
    if (target && Input.GetMouseButton(1)) {
        movX = Input.GetAxis("Mouse X")* movSpeed * 0.055;
        movY = Input.GetAxis("Mouse Y")* movSpeed * 0.055;
       if (movX > 0) target.Translate(Vector3.right * movX * Time.deltaTime);
       if (movX < 0) target.Translate(Vector3.left  *-movX * Time.deltaTime);
       if (movY < 0) target.Translate(Vector3.up    *-movY * Time.deltaTime);
       if (movY > 0) target.Translate(Vector3.down  * movY * Time.deltaTime);
 	}
 		
 		
	// 繝槭え繧ｹ縺ｮ遘ｻ蜍暮㍼繧偵き繝｡繝ｩ縺ｮ蝗櫁ｻ｢蛟､縺ｨ遘ｻ蜍募?､縺ｸ螟画鋤縺励※繧ｫ繝｡繝ｩ繧貞屓縺?    if (target && Input.GetMouseButton(0)) {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
 		
 		y = ClampAngle(y, yMinLimit, yMaxLimit);
 	}
 		       
    var rotation = Quaternion.Euler(y, x, 0);
    var position = rotation * Vector3(cameraBk.x, cameraBk.y, -distance) + target.position;
        
    transform.rotation = rotation;
    transform.position = position;
    target.LookAt(this.transform);   // 繧ｿ繝ｼ繧ｲ繝?ヨ繧偵き繝｡繝ｩ縺ｮ譁ｹ縺ｸ蜷代￠繧?}





// 荳企剞荳矩剞繝√ぉ繝?け(蝗櫁ｻ｢隗貞ｺｦ縺?360??60縺ｮ髢薙↓蜿弱∪繧九ｈ縺?↓縺吶ｋ)
static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}