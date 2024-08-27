<script>
	// Inicjalizacja sceny, kamery i renderera
	var scene = new THREE.Scene();
	var camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
	var renderer = new THREE.WebGLRenderer();
	renderer.setSize(window.innerWidth, window.innerHeight);
	document.body.appendChild(renderer.domElement);

	// Definicja geometrii pionka
	var geometry = new THREE.CylinderGeometry(0.5, 0.5, 1, 32);
	var material = new THREE.MeshPhongMaterial({ color: 0x000000 });
	var pawn = new THREE.Mesh(geometry, material);

	// Dodanie pionka do sceny
	scene.add(pawn);

	// Dodanie światła do sceny
	var light = new THREE.PointLight(0xffffff, 1, 100);
	light.position.set(10, 10, 10);
	scene.add(light);

	// Ustawienie pozycji kamery
	camera.position.z = 5;

	// Funkcja animacji
	function animate() {
		requestAnimationFrame(animate);
		renderer.render(scene, camera);
	}
	animate();

</script>